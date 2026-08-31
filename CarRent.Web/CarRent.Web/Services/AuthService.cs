using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using CarRent.Web.Authentication;
using CarRent.Web.Models.Auth;

namespace CarRent.Web.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly IJSRuntime _jsRuntime;

    public AuthService(
        HttpClient httpClient,
        AuthenticationStateProvider authStateProvider,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _jsRuntime = jsRuntime;
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", request);

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResult { IsSuccess = false, ErrorMessage = "Email sau parolă incorectă." };
            }

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (result == null || string.IsNullOrWhiteSpace(result.Token))
            {
                return new AuthResult { IsSuccess = false, ErrorMessage = "Răspuns invalid de la server." };
            }

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
            ((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);

            return new AuthResult { IsSuccess = true };
        }
        catch (Exception ex)
        {
            return new AuthResult { IsSuccess = false, ErrorMessage = $"Eroare de conexiune la server: {ex.Message}" };
        }
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", request);

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResult { IsSuccess = false, ErrorMessage = "Înregistrarea a eșuat. Verificați datele introduse." };
            }

            // Backend-ul returnează Guid (userId)
            await response.Content.ReadFromJsonAsync<Guid>();
            return new AuthResult { IsSuccess = true };
        }
        catch (Exception ex)
        {
            return new AuthResult { IsSuccess = false, ErrorMessage = $"Eroare de conexiune la server: {ex.Message}" };
        }
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        ((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
    }
}

public class AuthResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}