namespace CarRent.Web.Models.Ads;

public class AdImageDto
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }
}