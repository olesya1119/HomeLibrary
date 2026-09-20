using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Store;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string? _connectionString;

    public DbConnectionFactory()
    {
        Env.Load();

        _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("DB_CONNECTION_STRING is not configured.");
        }
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
