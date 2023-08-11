namespace Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;

using System;
using global::OpenTelemetry.Exporter;
using global::OpenTelemetry.Metrics;
using global::OpenTelemetry.Resources;
using global::OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        string? serviceName,
        Action<JaegerExporterOptions> configureJaeger,
        Action<TracerProviderBuilder>? configureBuilder = null,
        Action<MeterProviderBuilder>? configureMeter = null)
    {
        services
            .AddOpenTelemetry()
            .WithTracing
                (builder =>
                     {
                         builder
                             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                             .AddAspNetCoreInstrumentation(o => o.AddDefaultIgnorePatterns())
                             .AddHttpClientInstrumentation()
                             .AddJaegerExporter(configureJaeger);
                         configureBuilder?.Invoke(builder);
                     })
            .WithMetrics(builder =>
                             {
                                 builder.AddPrometheusExporter()
                                        .AddRuntimeInstrumentation()
                                        .AddAspNetCoreInstrumentation()
                                        .AddHttpClientInstrumentation();
                                 configureMeter?.Invoke(builder);
                             });

        return services;
    }
}