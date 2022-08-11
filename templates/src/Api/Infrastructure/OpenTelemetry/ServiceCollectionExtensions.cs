namespace Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;

using System;
using global::OpenTelemetry.Exporter;
using global::OpenTelemetry.Resources;
using global::OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        string? serviceName,
        Action<JaegerExporterOptions> configureJaeger,
        Action<TracerProviderBuilder>? configureBuilder = null)
    {
        return services
            .AddOpenTelemetryTracing
                (builder =>
                     {
                         builder
                             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                             .AddDefaultIgnorePatterns()
                             .AddJaegerExporter(configureJaeger);
                         configureBuilder?.Invoke(builder);
                     });
    }
}