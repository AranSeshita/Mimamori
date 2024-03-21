namespace Mimamori.Domains.Entities;

public class Presence : BaseAttributes
{
    public string TenantId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string JobId { get; set; } = null!;
    public string Availability { get; set; } = null!;
    public string Activity { get; set; } = null!;
}