using MGK.ServiceBase.Configuration.SeedWork;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MGK.ServiceBase.DAL.Infrastructure.Factories;

public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
	private readonly IConfigurationSetup _configurationSetup;
	private IDbConnection _connection;

    public SqlConnectionFactory(IConfigurationSetup configurationSetup)
	{
		_configurationSetup = configurationSetup;
    }

    public IDbConnection GetOpenConnection(string database)
    {
        if (_connection is null || _connection.State != ConnectionState.Open)
        {
            _connection = new SqlConnection(_configurationSetup.GetConnectionString(database));
            _connection.Open();
        }

        return _connection;
    }

    public int GetTimeoutForQueries(string database)
        => _configurationSetup.GetQueryTimeout(database);

    public void Dispose()
    {
        if (_connection?.State == ConnectionState.Open)
        {
            _connection.Dispose();
        }
    }
}
