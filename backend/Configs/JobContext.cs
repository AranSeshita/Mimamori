using Hangfire.Server;

namespace Mimamori.Configs;

public class JobContext : IServerFilter
{
    [ThreadStatic]
    private static string _jobId;
    [ThreadStatic]
    private static Type _jobType;

    public static string JobId { get { return _jobId; } set { _jobId = value; } }

    public static Type JobType { get { return _jobType; } set { _jobType = value; } }

    public void OnPerforming(PerformingContext context)
    {
        JobId = context.BackgroundJob.Id;
        JobType = context.BackgroundJob.Job.Type;
    }

    public void OnPerformed(PerformedContext filterContext)
    {
    }
}