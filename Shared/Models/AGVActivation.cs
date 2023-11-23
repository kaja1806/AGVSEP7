namespace Shared.Models;

public class AGVActivation
{
    public static bool IsToleranceOK { get; set; }
    public bool ActivateAGV => IsToleranceOK;

}


