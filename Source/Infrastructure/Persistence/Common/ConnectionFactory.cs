using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;

namespace Infrastructure.Persistence.Common;

public class ConnectionFactory
{
    // Given connection string,
    // Create the DbProviderFactory and DbConnection.
    // Returns a DbConnection on success; null on failure.
    public static NpgsqlConnection CreateDbConnection(string connectionString = null)
    {
        connectionString ??= ConfigManager.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

        // Create the DbProviderFactory and DbConnection.
        if (connectionString != null)
        {
            try
            {
                DbConnection connection;
                DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");
                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;

                // Return the connection.
                return (NpgsqlConnection)connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return null;
    }
}
