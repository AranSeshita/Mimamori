using Microsoft.Extensions.Options;
using Microsoft.Graph.Models;
using Mimamori.Configs;
using Mimamori.Domains.Repositories;
using Mimamori.Domains.Repositories.MongoDb;
using Mimamori.Infrastructures.Repositories.MongoDb;
using MongoDB.Driver;

namespace Mimamori.Infrastructures;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IPresenceRepository> _presenceRepository;
    private readonly IMongoDatabase _mongoDatabase;
    public RepositoryManager(
        IOptions<MongoDbSettings> options)
    {
        _mongoDatabase = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.DatabaseName);
        _presenceRepository = new Lazy<IPresenceRepository>(() => new PresenceRepository(_mongoDatabase));
    }
    public IPresenceRepository PresenceRepository => _presenceRepository.Value;
}