using System.Data;
using Npgsql;

namespace Pokedex.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource)
{
    public IDbConnection OpenConnection()
    {
        var connection = dataSource.OpenConnection();

        return connection;
    }
}