using Mimamori.Applications.Contracts;

namespace Mimamori.Applications.Grains.Abstractions;

public interface IPresenceGrain : IGrainWithGuidKey
{
    Task<PresenceDto> GetPresence(string userId);
    Task StorePresence(string tenantId, string userId);
}
