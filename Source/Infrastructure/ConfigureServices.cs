using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

        _ = services
            .AddTransient<IApplicationDbContext, ApplicationDbContext>()
            .AddTransient<IConfigurationRepository, ConfigurationRepository>(x => new(ConfigurationRepository["ConnectionStrings:DefaultConnection"]));

        return services;
    }
}
