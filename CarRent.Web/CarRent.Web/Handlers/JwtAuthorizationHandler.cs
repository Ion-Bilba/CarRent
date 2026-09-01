using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace CarRent.Web.Handlers;

public class JwtAuthorizationHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    public JwtAuthorizationHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Preluare token salvat la Login/Register
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}