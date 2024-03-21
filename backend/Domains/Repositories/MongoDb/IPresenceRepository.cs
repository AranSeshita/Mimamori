using Mimamori.Domains.Entities;

namespace Mimamori.Domains.Repositories.MongoDb;

public interface IPresenceRepository
{
    Task<List<Presence>> GetAllForTenantAsync(string tenantId);
    Task<Presence?> GetAllForUserAsync(string tenantId, string userId);
    Task CreateAsync(Presence presence);
    Task RemoveAsync(string tenantId, string userId);
}