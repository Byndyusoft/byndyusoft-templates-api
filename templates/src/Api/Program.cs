namespace Byndyusoft.Template.Api
{
    using Byndyusoft.MaskedSerialization.Serilog.Extensions;
    using Infrastructure.OpenTelemetry;
    using Logging.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Npgsql;
    using Serilog;

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
                           services.AddOpenTelemetry(serviceName,
                                                     context.Configuration.GetSection("OtlpExporterOptions").Bind,
                                                     builder => builder.AddNpgsql(),
                                                     builder => builder.AddTemplateMetrics());
                       })
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       })
                       .UseSerilog((context, configuration) => configuration
                                                                 .UseDefaultSettings(context.Configuration)
                                                                 .UseOpenTelemetryTraces()
                                                                 .WriteToOpenTelemetry()
                                                                 .WithMaskingPolicy());
        }
    }
}