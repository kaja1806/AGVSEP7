namespace AGV.Models;

public class AgvLogEventArgs : EventArgs
{
    public AgvLogEventArgs(AgvJsonFormatModel agvLog)
    {
        AgvLog = agvLog;
    }

    public AgvJsonFormatModel AgvLog { get; set; }
}