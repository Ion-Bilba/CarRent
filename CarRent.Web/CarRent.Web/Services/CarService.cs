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

    public async Task<PagedResult<CarDto>?> GetCarsAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PagedResult<CarDto>>($"api/Cars?PageNumber={pageNumber}&PageSize={pageSize}");
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
            return await _httpClient.GetFromJsonAsync<CarDto>($"api/Cars/{id}");
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
            var response = await _httpClient.PostAsJsonAsync("api/Cars", request);
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
            var response = await _httpClient.PutAsJsonAsync($"api/Cars/{id}", request);
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