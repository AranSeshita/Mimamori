using Microsoft.Extensions.Options;
using Mimamori.Applications.Contracts;
using Mimamori.Applications.Services.Abstractions;
using Mimamori.Configs;
using Mimamori.Domains.Entities;
using MongoDB.Driver;

namespace Mimamori.Applications.Services;

public class PresenceService : IPresenceService
{
    private readonly IMongoCollection<PresenceDto> _presencesCollection;

    public PresenceService(
        IOptions<PresenceDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _presencesCollection = mongoDatabase.GetCollection<PresenceDto>(options.Value.PresenceCollectionName);
    }

    public async Task<List<PresenceDto>> GetAsync() =>
        await _presencesCollection.Find(_ => true).ToListAsync();

    public async Task<PresenceDto?> GetAsync(Guid id) =>
        await _presencesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PresenceDto newPresence) =>
        await _presencesCollection.InsertOneAsync(newPresence);

    public async Task UpdateAsync(Guid id, PresenceDto updatedPresence) =>
        await _presencesCollection.ReplaceOneAsync(x => x.Id == id, updatedPresence);

    public async Task RemoveAsync(Guid id) =>
        await _presencesCollection.DeleteOneAsync(x => x.Id == id);
}