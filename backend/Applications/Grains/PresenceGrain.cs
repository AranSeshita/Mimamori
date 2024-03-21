using AutoMapper;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Mimamori.Applications.Contracts;
using Mimamori.Applications.Grains.Abstractions;
using Mimamori.Applications.Services;
using Mimamori.Applications.Services.Abstractions;
using Newtonsoft.Json;
using Orleans.Runtime;

namespace Mimamori.Applications.Grains;

public class PresenceGrain : Grain, IPresenceGrain
{
    private readonly IPresenceGrain _presenceGrain;
    private readonly GraphServiceClient _graphServiceClient;
    IPresenceService _presencesService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<PresenceGrain> _logger;
    public PresenceGrain(ILogger<PresenceGrain> logger, IConfiguration configuration, IPresenceService presenceService, IClusterClient client, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
        _presencesService = presenceService;
        _presenceGrain = client.GetGrain<IPresenceGrain>(Guid.NewGuid());
    }
    public async Task<PresenceDto> GetPresence(string userId)
    {
        var presence = await _graphServiceClient.Users[userId].Presence.GetAsync();
        return _mapper.Map<PresenceDto>(presence);
    }
    public async Task StorePresence(string tenantId, string userId, string jobId)
    {
        var client = InitializeGraphClient(tenantId);
        _logger.LogInformation($"Grain called: {_presenceGrain.GetGrainId()}");
        var presence = await client.Users[userId].Presence.GetAsync();
        var result = _mapper.Map<PresenceDto>(presence);
        result.TenantId = tenantId;
        result.JobId = jobId;
        await _presencesService.CreateAsync(result);
        _logger.LogInformation("Stored: " + JsonConvert.SerializeObject(result));
    }

    public GraphServiceClient InitializeGraphClient(string tenantId)
    {
        var clientSecretCredential = new ClientSecretCredential(
            tenantId,
            _configuration.GetValue<string>("ClientId"),
            _configuration.GetValue<string>("ClientSecret"));
        var client = new GraphServiceClient(clientSecretCredential, new[] { "https://graph.microsoft.com/.default" });
        _logger.LogInformation("GraphServiceClient initialized");
        return client;
    }
}