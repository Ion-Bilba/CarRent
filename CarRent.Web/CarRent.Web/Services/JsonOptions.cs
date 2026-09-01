using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarRent.Web.Services;

/// <summary>
/// The API serializes enums as strings (JsonStringEnumConverter is registered
/// in CarRent.API/Program.cs). The default options used by HttpClient's
/// GetFromJsonAsync/PostAsJsonAsync/etc. do NOT know how to convert a JSON
/// string into a C# enum (or int), so every response containing an enum
/// (AdType, TractionType, FuelType, Transmission...) fails to deserialize
/// unless we pass these options explicitly.
/// </summary>
public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };
}