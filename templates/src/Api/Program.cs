namespace Byndyusoft.Template.Api
{
    using Byndyusoft.Logging.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Byndyusoft.MaskedSerialization.Serilog.Extensions;
    using Infrastructure.OpenTelemetry;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using Serilog.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var serviceName = typeof(Program).Assembly.GetName().Name;

            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices((context, services) =>
                                              {
                                                  services.ConfigureStaticTelemetryItemCollector()
                                                         .WithBuildConfiguration()
                                                         .WithAspNetCoreEnvironment()
                                                         .WithServiceName(serviceName);
                                                  services.AddOpenTelemetry(serviceName,
                                                                            context.Configuration.GetSection("OtlpExporterOptions").Bind,
                                                                            builder => builder.AddNpgsql(),
                                                                            builder => builder.AddTemplateMetrics());
                                              })
                       .ConfigureWebHostDefaults(webBuilder =>
                                                     {
                                                         webBuilder.UseStartup<Startup>();
                                                         webBuilder.UseSerilog((context, configuration) => configuration
                                                                                                           .UseDefaultSettings(context.Configuration)
                                                                                                           .Enrich.WithStaticTelemetryItems()
                                                                                                           .Enrich.WithPropertyDataAccessor()
                                                                                                           .UseOpenTelemetryTraces()
                                                                                                           .WriteToOpenTelemetry()
                                                                                                           .WithMaskingPolicy());
                                                     });
        }
    }
}