namespace Byndyusoft.Template.Api.Infrastructure.Metrics
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Prometheus;

    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestsMetrics(this IApplicationBuilder app)
        {
            return app
                   .UseHttpMetrics()
                   .Use(
                       async (context, next) =>
                       {
                           try
                           {
                               await next.Invoke();
                           }
                           catch (OperationCanceledException)
                           {
                               context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                               throw;
                           }
                           catch (Exception)
                           {
                               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                               throw;
                           }
                       }
                   );
        }
    }

}