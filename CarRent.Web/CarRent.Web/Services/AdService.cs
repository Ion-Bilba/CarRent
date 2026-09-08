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

    public async Task<PagedResult<AdDto>?> GetAdsAsync(int pageNumber = 1, int pageSize = 10, AdFilter? filter = null)
    {
        try
        {
            var query = new List<string> { $"PageNumber={pageNumber}", $"PageSize={pageSize}" };

            if (filter != null)
            {
                if (filter.AdType.HasValue) query.Add($"AdType={filter.AdType.Value}");
                if (filter.TractionType.HasValue) query.Add($"TractionType={filter.TractionType.Value}");
                if (filter.FuelType.HasValue) query.Add($"FuelType={filter.FuelType.Value}");
                if (filter.Transmission.HasValue) query.Add($"Transmission={filter.Transmission.Value}");
                if (!string.IsNullOrWhiteSpace(filter.Brand)) query.Add($"Brand={Uri.EscapeDataString(filter.Brand)}");
                if (!string.IsNullOrWhiteSpace(filter.Model)) query.Add($"Model={Uri.EscapeDataString(filter.Model)}");
                if (filter.MinYear.HasValue) query.Add($"MinYear={filter.MinYear.Value}");
                if (filter.MaxYear.HasValue) query.Add($"MaxYear={filter.MaxYear.Value}");
                if (filter.MinPrice.HasValue) query.Add($"MinPrice={filter.MinPrice.Value}");
                if (filter.MaxPrice.HasValue) query.Add($"MaxPrice={filter.MaxPrice.Value}");
            }

            var url = $"api/Ads?{string.Join("&", query)}";
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