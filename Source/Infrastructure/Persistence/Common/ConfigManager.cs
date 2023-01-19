using Cinis.PostgreSql;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Common;

public sealed class ConfigManager
{
    private static readonly Lazy<IConfiguration> _instanceConfiguration =
        new(() =>
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        });

    private static readonly Lazy<List<Configuration>> _instanceConfigurationDb =
        new(() =>
        {
            using var connection = ConnectionFactory.CreateDbConnection("");
            connection.Open();
            var response = connection.Read<Configuration>(whereClause: "is_active = '1'");
            return response;
        });

    private ConfigManager() { }

    public static IConfiguration Configuration => _instanceConfiguration.Value;

    public static List<Configuration> ConfigurationDb => _instanceConfigurationDb.Value;

    public static T GetValue<T>(string key)
    {
        // First try to retrieve the value from appsettings.json
        T value = Configuration.GetValue<T>(key);

        return value;
    }

    public static List<Configuration> GetValue(string configType = null, string configValue1 = null, string configValue2 = null, string key = null)
    {
        if (ConfigurationDb != null)
        {
            List<Configuration> tempList = new(ConfigurationDb);

            if (!string.IsNullOrEmpty(configType))
            {
                tempList = tempList.Where(x => x.ConfigType == configType)?.ToList();
            }

            if (!string.IsNullOrEmpty(configValue1))
            {
                tempList = tempList.Where(x => x.ConfigValue1 == configValue1)?.ToList();
            }

            if (!string.IsNullOrEmpty(configValue2))
            {
                tempList = tempList.Where(x => x.ConfigValue2 == configValue2)?.ToList();
            }

            if (!string.IsNullOrEmpty(key))
            {
                tempList = tempList.Where(x => x.Key == key)?.ToList();
            }

            return tempList;
        }

        return null;
    }
}
