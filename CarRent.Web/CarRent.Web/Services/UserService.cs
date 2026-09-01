using System.Net.Http.Json;
using CarRent.Web.Models.Users;

namespace CarRent.Web.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<UserDto>?> GetAllUsersAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}