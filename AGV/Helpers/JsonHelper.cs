using System.Text.Json;

namespace AGV.Helpers;

public static class JsonHelper
{
    public static string SerializeObject(object obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }

    public static T DeserializeObject<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}