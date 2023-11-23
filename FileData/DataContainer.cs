using System.Collections.Generic;
using Shared.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<AGVActivation> AGVActivations { get; set; }

    public DataContainer()
    {
        AGVActivations = new List<AGVActivation>();
    }
}