using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.Logging;
using Microsoft.AspNetCore.Mvc;
using Mimamori.Applications.Contracts;
using Mimamori.Applications.Grains.Abstractions;
using Mimamori.Applications.Services;
using Newtonsoft.Json;

namespace Mimamori.Presentations.Controllers;

[Route("api/[controller]/[action]")]
public class PresenceController : Controller
{
    private readonly IPresenceGrain _presenceGrain;
    private readonly ILogger<PresenceController> _logger;
    public PresenceController(IClusterClient client, ILogger<PresenceController> logger)
    {
        _presenceGrain = client.GetGrain<IPresenceGrain>(Guid.NewGuid());
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        _logger.LogInformation($"Grain called: {_presenceGrain.GetGrainId()}");
        var result = await _presenceGrain.GetPresence(userId);
        _logger.LogInformation(JsonConvert.SerializeObject(result));
        return Ok(result);
    }

    [HttpGet("{tenantId}/{userId}/{delayMinute}")]
    public async Task<IActionResult> CreateSchedule(string tenantId, string userId, int delayMinute)
    {
        BackgroundJob.Schedule(
            () => Create(tenantId, userId),
           TimeSpan.FromMinutes(delayMinute));
        return Ok();
    }
    public void Create(string tenantId, string userId)
    {
        RecurringJob.AddOrUpdate(
            $"Store Presence ({userId} in tenant:{tenantId})",
            () => Store(tenantId, userId),
            "*/10 * * * * *");
    }
    public async Task Store(string tenantId, string userId)
    {
        await _presenceGrain.StorePresence(tenantId, userId);
    }
}