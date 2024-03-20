namespace Mimamori.Domains.Entities;

public class Presence : BaseAttributes
{
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }
    public string Availability { get; set; } = null!;
    public string Activity { get; set; } = null!;
}