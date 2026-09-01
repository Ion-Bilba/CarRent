using System.Net.Http.Json;
using CarRent.Web.Models.Ads;

namespace CarRent.Web.Services;

public class FavoriteService
{
    private readonly HttpClient _httpClient;

    public FavoriteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AdDto>?> GetMyFavoritesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<AdDto>>("api/favorites", JsonOptions.Default);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsFavoriteAsync(Guid adId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/favorites/{adId}/status");
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddFavoriteAsync(Guid adId)
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/favorites/{adId}", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteFavoriteAsync(Guid adId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/favorites/{adId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}