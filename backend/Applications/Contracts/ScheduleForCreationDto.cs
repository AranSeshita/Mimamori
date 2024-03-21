namespace Mimamori.Applications.Contracts;

public class ScheduleForCreationDto
{
    public string TenantId { get; set; }
    public string UserId { get; set; }
    public DateTime startOn { get; set; }
    public DateTime endOn { get; set; }
    public long GetStartOnTimeSpan()
    {
        long unixTimeStartOn = (int)startOn.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        long unixTimeNow = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        if (unixTimeStartOn > unixTimeNow) return unixTimeStartOn - unixTimeNow;
        return 0;
    }
    public long GetEndOnTimeSpan()
    {
        long unixTimeEndOn = (int)endOn.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        long unixTimeNow = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        if (unixTimeEndOn > unixTimeNow) return unixTimeEndOn - unixTimeNow;
        return 0;
    }
}