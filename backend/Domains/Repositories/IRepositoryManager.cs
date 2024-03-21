using Mimamori.Domains.Repositories.MongoDb;

namespace Mimamori.Domains.Repositories;

public interface IRepositoryManager
{
    IPresenceRepository PresenceRepository { get; }
}