using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddAutoMapper(typeof(Program));
        services.AddControllers(options =>
        {
            options.Filters.Add<BaseExceptionFilter>();
        });
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new QueryStringApiVersionReader("x-api-version"),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}