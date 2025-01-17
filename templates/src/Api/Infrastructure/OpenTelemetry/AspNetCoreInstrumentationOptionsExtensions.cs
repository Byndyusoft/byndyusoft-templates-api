﻿namespace Byndyusoft.Template.Api.Infrastructure.OpenTelemetry;

using System.Linq;
using global::OpenTelemetry.Instrumentation.AspNetCore;

public static class AspNetCoreInstrumentationOptionsExtensions
{
    public static AspNetCoreTraceInstrumentationOptions AddDefaultIgnorePatterns(this AspNetCoreTraceInstrumentationOptions options)
    {
        var ignoredSegments = new[] { "/swagger", "/favicon", "/healthz", "/metrics" };
        options.Filter = context => ignoredSegments.All(s => context.Request.Path.StartsWithSegments(s) == false);
        return options;
    }
}