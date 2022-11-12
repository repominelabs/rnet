using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Presentation.Filters;
using Presentation.Handlers;

namespace Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        #region Basic Authentication Configurations
        services.AddAuthentication(BasicAuthenticationHandler.SchemeName)
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationHandler.SchemeName, null);
        #endregion

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

        #region Swagger Configurations
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "basic"
                                    }
                                },
                                Array.Empty<string>()
                            }
                        });
            options.CustomSchemaIds(type => type.ToString());
            options.CustomSchemaIds(type => type.FullName);
            options.OperationFilter<SwaggerDefaultValuesFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "DotNetKickoff API" });
            options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "DotNetKickoff API" });
        });
        #endregion

        return services;
    }
}