using CarRent.Web;
using CarRent.Web.Authentication;
using CarRent.Web.Handlers;
using CarRent.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7088/";


builder.Services.AddTransient<CarRent.Web.Handlers.JwtAuthorizationHandler>();
builder.Services.AddTransient<HttpErrorInterceptorHandler>();

builder.Services.AddHttpClient("CarRent.API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<CarRent.Web.Handlers.JwtAuthorizationHandler>()
.AddHttpMessageHandler<HttpErrorInterceptorHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CarRent.API"));
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<AdImageService>();

await builder.Build().RunAsync();