using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Database.SQLHelper;

public class SqlConnectionClass 
{
    private readonly IConfiguration _configuration;
    private SqlConnection _databaseConnection;

    public SqlConnectionClass(IConfiguration configuration, SqlConnection databaseConnection)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _databaseConnection = databaseConnection;
    }

    public SqlConnection GetConnection()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection") ??
                                  throw new Exception(" Database connection error");
        _databaseConnection = new SqlConnection(connectionString);
        _databaseConnection.Open();

        if (_databaseConnection.State != System.Data.ConnectionState.Open)
        {
            throw new Exception($"Connection state: {_databaseConnection.State}");
        }

        return _databaseConnection;
    }
}