namespace Mimamori.Applications.Contracts;

[GenerateSerializer]
public class PresenceDto
{
    [Id(0)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Id(1)]
    public string UserId { get; set; } = null!;
    [Id(2)]
    public string TenantId { get; set; } = null!;
    [Id(3)]
    public string JobId { get; set; } = null!;
    [Id(4)]
    public string Availability { get; set; } = null!;
    [Id(5)]
    public string Activity { get; set; } = null!;
}