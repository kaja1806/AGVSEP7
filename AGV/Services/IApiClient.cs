namespace AGV.Services;

public interface IApiClient
{
    Task SendLogToWebApi(string statorId, string json);
    Task<HttpResponseMessage> GetSegmentCoordinates();
}