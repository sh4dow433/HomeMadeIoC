using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.ConfigurationParser;

internal static class JsonFileParser
{
    public static List<Service> GetServicesFromFile(string path)
    {
        var rawJson = File.ReadAllText(path);
        var result = JsonConvert.DeserializeObject<List<Service>>(rawJson);
        if (result == null)
        {
            throw new Exception("File couldn't be parsed");
        }
        return result;
    }
}
