using Byndyusoft.Logging.Builders;
using Byndyusoft.Logging.Configuration;
using Byndyusoft.MaskedSerialization.Serilog.Extensions;
using Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;
using Byndyusoft.Template.Api.Infrastructure.Serialization;
using Byndyusoft.Template.Api.Infrastructure.Swagger;
using Byndyusoft.Template.Api.Infrastructure.Versioning;
using Byndyusoft.Template.Api.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;
using Serilog.Configuration;

var serviceName = typeof(Program).Assembly.GetName().Name;
var serviceVersion = typeof(Program).Assembly.GetName().Version;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(
    (context, configuration) =>
        configuration
            .UseDefaultSettings(context.Configuration)
            .UseOpenTelemetryTraces()
            .WriteToOpenTelemetry(activityEventBuilder: StructuredActivityEventBuilder.Instance)
            .WithMaskingPolicy()
            .Enrich.WithPropertyDataAccessor()
            .Enrich.WithStaticTelemetryItems()
);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOpenTelemetry(
    serviceName,
    builder.Configuration.GetSection("OtlpExporterOptions").Bind,
    builder => builder.AddNpgsql(),
    builder => builder.AddTemplateMetrics()
);
services
    .AddMvcCore()
    .AddTracing();
services
    .AddRouting(options => options.LowercaseUrls = true)
    .AddJsonSerializerOptions();
services.AddHealthChecks();
services
    .ConfigureStaticTelemetryItemCollector()
    .WithBuildConfiguration()
    .WithAspNetCoreEnvironment()
    .WithServiceName(serviceName)
    .WithApplicationVersion(serviceVersion.ToString());
services
    .AddVersioning()
    .AddSwagger();
services
    .AddRelationalDb(NpgsqlFactory.Instance, builder.Configuration.GetConnectionString("Main"))
    .AddApplicationServices();

var app = builder.Build();
app
    .UseHealthChecks("/healthz")
    .UseOpenTelemetryPrometheusScrapingEndpoint()
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

// For tests accessibility
public partial class Program { }