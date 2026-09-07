namespace CarRent.Web.Services;
public static class ImageUrlHelper
{
    public static string ApiBaseUrl { get; set; } = string.Empty;

    public const string PlaceholderUrl =
        "data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='400' height='300'><rect width='100%25' height='100%25' fill='%23e9ecef'/><text x='50%25' y='50%25' font-family='sans-serif' font-size='16' fill='%236c757d' text-anchor='middle' dominant-baseline='middle'>Fara imagine</text></svg>";

    public static string Resolve(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return PlaceholderUrl;

        if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return url;
        }

        return $"{ApiBaseUrl.TrimEnd('/')}/{url.TrimStart('/')}";
    }
}