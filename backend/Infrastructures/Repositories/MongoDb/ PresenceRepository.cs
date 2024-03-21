using Mimamori.Domains.Entities;
using Mimamori.Domains.Repositories;
using Mimamori.Domains.Repositories.MongoDb;
using MongoDB.Driver;

namespace Mimamori.Infrastructures.Repositories.MongoDb;

internal sealed class PresenceRepository : IPresenceRepository
{
    private readonly IMongoCollection<Presence> _collection;
    public PresenceRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Presence>(nameof(Presence));
    }

    public async Task CreateAsync(Presence newPresence)
    {
        await _collection.InsertOneAsync(newPresence);
    }

    public async Task<List<Presence>> GetAllForTenantAsync(string tenantId)
    {
        return await _collection.Find(x => x.TenantId == tenantId).ToListAsync();
    }

    public async Task<Presence?> GetAllForUserAsync(string tenantId, string userId)
    {
        return await _collection.Find(x => x.TenantId == tenantId).FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(string tenantId, string userId)
    {
        throw new NotImplementedException();
    }
}