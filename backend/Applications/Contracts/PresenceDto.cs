namespace Mimamori.Applications.Contracts;

[GenerateSerializer]
public class PresenceDto
{
    [Id(0)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    [Id(1)]
    public string Availability { get; set; } = null!;
    [Id(2)]
    public string Activity { get; set; } = null!;

}