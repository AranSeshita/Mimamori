namespace Mimamori.Configs;

public class PresenceDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PresenceCollectionName { get; set; } = null!;
}