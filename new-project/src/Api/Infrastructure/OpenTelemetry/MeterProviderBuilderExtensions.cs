namespace Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;

using global::OpenTelemetry.Metrics;

public static class MeterProviderBuilderExtensions
{
    public static MeterProviderBuilder AddTemplateMetrics(this MeterProviderBuilder builder)
    {
        return builder
               .AddMeter(ApiTemplateMetrics.Name)
               .AddHistogramView(ApiTemplateMetrics.DurationName, ApiTemplateMetrics.DurationBuckets);
    }

    private static MeterProviderBuilder AddHistogramView(this MeterProviderBuilder builder, string instrumentName, double[] boundaries)
    {
        return builder
            .AddView(instrumentName,
                     new ExplicitBucketHistogramConfiguration
                         {
                             Boundaries = boundaries
                         });
    }
}