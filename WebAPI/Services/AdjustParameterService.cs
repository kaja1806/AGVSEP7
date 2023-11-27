using Dapper;
using Database.SQLHelper;
using Shared.Models;

namespace WebAPI.Services;

public class AdjustParameterService : IAdjustParameterService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public AdjustParameterService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    public string AdjustParameters(StatorModel realTimeData)
    {
        using var connection = _sqlConnectionClass.GetConnection();
        List<StatorModel> result = connection.Query<StatorModel>(@"SELECT AirGap, TargetValue FROM Stator").ToList();

        var airgaps = result.Select(x => x.AirGap).ToList();

        // logic for adjusting parameters based on real-time data
        double deviation = realTimeData.AirGap - realTimeData.TargetValue;

        // adjust parameters based on deviation
        if (deviation > 0.2)
        {
            return AdjustParameter("parameter to increase");
        }
        else if (deviation < -0.2)
        {
            return AdjustParameter("parameter to decrease");
        }

        return AdjustParameter("has been successful!");
    }

    private string AdjustParameter(string parameterName)
    {
        return $"Adjusting {parameterName}";
    }
}