using System.Text.Json.Serialization;

namespace Shared.Models;

public class ApiResponseModel
{
    [JsonPropertyName("value")] public string Value { get; set; }
    [JsonPropertyName("statusCode")] public int StatusCode { get; set; }
}