using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Presentation;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Added for App Security
app.UseMiddleware(typeof(SecurityMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"DotNetKickoff - {description.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
