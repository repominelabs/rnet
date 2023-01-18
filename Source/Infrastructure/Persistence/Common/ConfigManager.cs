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
            List<Configuration> configurations = new();

            // Todo

            return configurations;
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
}
