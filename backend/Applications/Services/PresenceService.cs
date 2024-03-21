using AutoMapper;
using Microsoft.Extensions.Options;
using Mimamori.Applications.Contracts;
using Mimamori.Applications.Services.Abstractions;
using Mimamori.Configs;
using Mimamori.Domains.Entities;
using Mimamori.Domains.Repositories;
using MongoDB.Driver;

namespace Mimamori.Applications.Services;

public class PresenceService : IPresenceService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public PresenceService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<List<PresenceDto>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PresenceDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task CreateAsync(PresenceDto presenceDto)
    {
        var presence = _mapper.Map<Presence>(presenceDto);
        await _repositoryManager.PresenceRepository.CreateAsync(presence);
    }
}