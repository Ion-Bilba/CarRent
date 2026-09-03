using System.Net.Http.Json;
using CarRent.Web.Models.Cars;
using CarRent.Web.Models.Common;

namespace CarRent.Web.Services;

public class CarService
{
    private readonly HttpClient _httpClient;

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CarDto>> GetMyCarsAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<CarDto>>("api/Cars/mine", JsonOptions.Default);
            return result ?? new List<CarDto>();
        }
        catch
        {
            return new List<CarDto>();
        }
    }

    public async Task<List<CarDto>?> GetCarsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<CarDto>>("api/Cars", JsonOptions.Default);
        }
        catch
        {
            return null;
        }
    }

    public async Task<CarDto?> GetCarByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CarDto>($"api/Cars/{id}", JsonOptions.Default);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Guid?> CreateCarAsync(CreateCarRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cars", request, JsonOptions.Default);
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

    public async Task<bool> UpdateCarAsync(Guid id, UpdateCarRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cars/{id}", request, JsonOptions.Default);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteCarAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Cars/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}