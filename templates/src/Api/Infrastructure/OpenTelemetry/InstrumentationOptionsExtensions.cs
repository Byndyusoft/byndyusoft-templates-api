namespace Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;

using System.Linq;
using Execution.Metrics.AspNet;
using global::OpenTelemetry.Instrumentation.AspNetCore;
using Microsoft.AspNetCore.Http;

public static class InstrumentationOptionsExtensions
{
    private static readonly string[] IgnoredSegments = { "/swagger", "/favicon", "/healthz", "/metrics" };

    public static AspNetCoreTraceInstrumentationOptions AddDefaultIgnorePatterns(
        this AspNetCoreTraceInstrumentationOptions options)
    {
        options.Filter = FilterQueries;
        return options;
    }

    public static HttpRequestExecutionDurationInstrumentationOptions AddDefaultIgnorePatterns(
        this HttpRequestExecutionDurationInstrumentationOptions options)
    {
        options.Filter = FilterQueries;
        return options;
    }

    private static bool FilterQueries(HttpContext httpContext)
    {
        return IgnoredSegments.All(s => httpContext.Request.Path.StartsWithSegments(s) == false);
    }
}