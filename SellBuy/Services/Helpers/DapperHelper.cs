using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data.Common;

public class DapperHelper
{
    private readonly string _connectionString;
    private readonly IHostEnvironment _hostEnvironment;

    public DapperHelper(IOptions<DbOptions> config, IHostEnvironment hostEnvironment)
    {
        _connectionString = config.Value.CONNECTION_STRING;
        _hostEnvironment = hostEnvironment;
    }

    public DbConnection GetConnection()
    {
        if (_hostEnvironment.IsDevelopment())
            return new ProfiledDbConnection(new SqlConnection(_connectionString), MiniProfiler.Current);
        else
            return new SqlConnection(_connectionString);
    }
}
