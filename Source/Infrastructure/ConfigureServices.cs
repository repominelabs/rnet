using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _ = services
                .AddTransient<ICommentRepository, CommentRepository>(x => new CommentRepository(""))
                .AddTransient<IPostRepository, PostRepository>(x => new PostRepository(""))
                .AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
