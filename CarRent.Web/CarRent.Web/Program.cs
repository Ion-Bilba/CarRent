using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using CarRent.Web;
using CarRent.Web.Authentication;
using CarRent.Web.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7088";

// Inregistrare Handlere si Servicii
builder.Services.AddTransient<JwtAuthorizationHandler>();

builder.Services.AddHttpClient("CarRent.API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();

// Configurare Client implicit pentru aplicatie
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CarRent.API"));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<FavoriteService>();

await builder.Build().RunAsync();