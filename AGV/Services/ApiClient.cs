using System.Text;

namespace AGV.Services;

public class ApiClient : IApiClient
{
    private readonly HttpClient httpClient;

    public ApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri("http://localhost:5004/");
    }

    public async Task SendLogToWebApi(string statorId, string json)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await httpClient.PostAsync($"api/Agv/SaveAgvStatusLogs/{statorId}", content);
    }

    public async Task<HttpResponseMessage> GetSegmentCoordinates()
    {
        return await httpClient.GetAsync($"api/SaveAgvStatusLogs/");
    }
}