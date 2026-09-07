using System.Net.Http.Json;
using CarRent.Web.Models.Ads;
using CarRent.Web.Models.Common;

namespace CarRent.Web.Services;

public class AdService
{
    private readonly HttpClient _httpClient;

    public AdService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PagedResult<AdDto>?> GetAdsAsync(int pageNumber = 1, int pageSize = 10, AdType? adType = null)
    {
        try
        {
            var url = $"api/Ads?PageNumber={pageNumber}&PageSize={pageSize}";
            if (adType.HasValue)
            {
                url += $"&AdType={adType.Value}";
            }

            return await _httpClient.GetFromJsonAsync<PagedResult<AdDto>>(url, JsonOptions.Default);
        }
        catch
        {
            return null;
        }
    }

    public async Task<AdDto?> GetAdByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<AdDto>($"api/Ads/{id}", JsonOptions.Default);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Guid?> CreateAdAsync(CreateAdRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ads", request, JsonOptions.Default);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Guid>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateAdAsync(Guid id, UpdateAdRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ads/{id}", request, JsonOptions.Default);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAdAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Ads/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<AdDto>> GetMyAdsAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<AdDto>>("api/Ads/mine", JsonOptions.Default);
            return result ?? new List<AdDto>();
        }
        catch
        {
            return new List<AdDto>();
        }
    }
}