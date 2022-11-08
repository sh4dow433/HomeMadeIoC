using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.ConfigurationParser;

internal class Service
{
    public string? Abstraction { get; set; }
    public string Type { get; set; } = null!;
    public bool IsSingleton { get; set; } = false;
}
