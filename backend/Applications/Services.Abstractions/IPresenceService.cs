using Mimamori.Applications.Contracts;

namespace Mimamori.Applications.Services.Abstractions;

public interface IPresenceService
{
    Task<List<PresenceDto>> GetAsync();

    Task<PresenceDto?> GetAsync(Guid id);

    Task CreateAsync(PresenceDto newPresence);

    Task UpdateAsync(Guid id, PresenceDto updatedPresence);

    Task RemoveAsync(Guid id);
}