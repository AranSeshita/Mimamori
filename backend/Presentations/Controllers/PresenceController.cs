using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using Mimamori.Applications.Contracts;
using Mimamori.Applications.Grains.Abstractions;
using Mimamori.Configs;
using MongoDB.Bson.IO;

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

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleForCreationDto scheduleForCreationDto)
    {
        var referenceId = Guid.NewGuid().ToString();
        BackgroundJob.Schedule(() => Create(referenceId, scheduleForCreationDto.TenantId, scheduleForCreationDto.UserId), TimeSpan.FromSeconds(scheduleForCreationDto.GetStartOnTimeSpan()));
        BackgroundJob.Schedule(() => Delete(referenceId, scheduleForCreationDto.UserId), TimeSpan.FromSeconds(scheduleForCreationDto.GetEndOnTimeSpan()));
        return Ok($"Scheduled! ReferenceId is {referenceId}:{scheduleForCreationDto.UserId}");
    }
    public void Create(string referenceId, string tenantId, string userId)
    {
        RecurringJob.AddOrUpdate(
            $"{userId}:{referenceId}",
            () => Store(tenantId, userId, JobContext.JobId),
            "*/10 * * * * *");
    }
    public void Delete(string referenceId, string userId)
    {
        RecurringJob.RemoveIfExists($"{userId}:{referenceId}");
    }
    public async Task Store(string tenantId, string userId, string jobId)
    {
        await _presenceGrain.StorePresence(tenantId, userId, jobId);
    }
}