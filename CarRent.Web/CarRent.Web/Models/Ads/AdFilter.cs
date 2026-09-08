using CarRent.Web.Models.Cars;

namespace CarRent.Web.Models.Ads;

public class AdFilter
{
    public AdType? AdType { get; set; }
    public TractionType? TractionType { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public FuelType? FuelType { get; set; }
    public Transmission? Transmission { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public bool HasAnyFilter =>
        TractionType.HasValue || !string.IsNullOrWhiteSpace(Brand) || !string.IsNullOrWhiteSpace(Model) ||
        MinYear.HasValue || MaxYear.HasValue || FuelType.HasValue || Transmission.HasValue ||
        MinPrice.HasValue || MaxPrice.HasValue;
}