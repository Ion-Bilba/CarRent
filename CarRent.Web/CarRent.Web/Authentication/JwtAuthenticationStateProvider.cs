using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace CarRent.Web.Authentication;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public JwtAuthenticationStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(_anonymous);
            }

            var claims = ParseClaimsFromJwt(token);

            var expClaim = claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expClaim != null && long.TryParse(expClaim, out var expSeconds))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                if (expirationTime <= DateTimeOffset.UtcNow)
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                    return new AuthenticationState(_anonymous);
                }
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        NotifyAuthenticationStateChanged(authState);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var parts = jwt.Split('.');
        if (parts.Length < 2) return claims;

        var payload = parts[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            foreach (var kvp in keyValuePairs)
            {
                var key = kvp.Key;

                if (key == "role" || key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                {
                    AddRoleClaim(claims, kvp.Value);
                }
                else if (key == "sub" || key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, kvp.Value.ToString()!));
                }
                else if (key == "email" || key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                {
                    claims.Add(new Claim(ClaimTypes.Email, kvp.Value.ToString()!));
                }
                else
                {
                    claims.Add(new Claim(key, kvp.Value.ToString()!));
                }
            }
        }
        return claims;
    }

    private static void AddRoleClaim(List<Claim> claims, object value)
    {
        if (value is JsonElement element && element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
            }
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, value.ToString()!));
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}