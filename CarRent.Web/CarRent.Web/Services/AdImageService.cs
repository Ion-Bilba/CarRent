using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace CarRent.Web.Services;

public class AdImageService
{
    private readonly HttpClient _httpClient;

  
    public const long MaxFileSizeBytes = 5 * 1024 * 1024;
    public static readonly string[] AllowedContentTypes =
    {
        "image/jpeg", "image/jpg", "image/png", "image/webp"
    };

    public AdImageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid?> UploadImageAsync(Guid adId, IBrowserFile file, int displayOrder)
    {
        try
        {
            if (file.Size > MaxFileSizeBytes)
                return null;

            if (!AllowedContentTypes.Contains(file.ContentType))
                return null;

            using var content = new MultipartFormDataContent();
            await using var stream = file.OpenReadStream(MaxFileSizeBytes);
            using var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(streamContent, "File", file.Name);
            content.Add(new StringContent(displayOrder.ToString()), "DisplayOrder");

            var response = await _httpClient.PostAsync($"api/ads/{adId}/images", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<UploadResult>();
            return result?.Id;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteImageAsync(Guid adId, Guid imageId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ads/{adId}/images/{imageId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private class UploadResult
    {
        public Guid Id { get; set; }
    }
}