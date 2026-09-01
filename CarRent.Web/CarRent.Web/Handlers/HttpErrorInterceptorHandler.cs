using System.Net;
using CarRent.Web.Services;
using Microsoft.AspNetCore.Components;

namespace CarRent.Web.Handlers;

public class HttpErrorInterceptorHandler : DelegatingHandler
{
    private readonly NotificationService _notificationService;
    private readonly NavigationManager _navigationManager;

    public HttpErrorInterceptorHandler(NotificationService notificationService, NavigationManager navigationManager)
    {
        _notificationService = notificationService;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest: 
                    _notificationService.ShowError("Datele introduse sunt invalide. Verifică câmpurile din formular.");
                    break;

                case HttpStatusCode.Unauthorized: 
                    _notificationService.ShowError("Sesiunea a expirat sau nu ești autentificat.");
                    _navigationManager.NavigateTo("login");
                    break;

                case HttpStatusCode.Forbidden: 
                    _notificationService.ShowError("❌ Nu ai permisiunea necesară pentru a efectua această acțiune.");
                    break;

                case HttpStatusCode.NotFound: 
                    _notificationService.ShowError("Resursa solicitată nu a fost găsită.");
                    break;

                case HttpStatusCode.Conflict:
                    _notificationService.ShowError("A apărut un conflict la procesarea cererii.");
                    break;

                case HttpStatusCode.InternalServerError: 
                    _notificationService.ShowError("A apărut o eroare internă pe server. Încearcă din nou mai târziu.");
                    break;

                default:
                    _notificationService.ShowError($"A apărut o eroare neașteptată (Cod {(int)response.StatusCode}).");
                    break;
            }
        }

        return response;
    }
}