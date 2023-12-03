namespace Shared.Enums;

public class StatusEnums
{
    public enum SegmentStatus
    {
        Waiting = 0,
        Transport = 1,
        Installation = 2,
        Installed = 3, // when no adjustment needed
        Adjusted = 4 //only when report showed 'Adjusted needed'
    }

    public enum AGVStatus
    {
        Home = 0,
        Transporting = 1,
        Installing = 2,
        Installed = 3, // when no adjustment needed
    }
}