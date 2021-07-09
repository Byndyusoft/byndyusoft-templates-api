namespace Byndyusoft.Template.Api
{
    using Byndyusoft.Logging.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Byndyusoft.Tracing;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices((context, services) =>
                       {
                           services.AddOpenTracingServices(options => options.AddDefaultIgnorePatterns()
                                                                             .WithDefaultOperationNameResolver())
                                   .AddJaegerTracer(context.Configuration);
                       })
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                           webBuilder.UseSerilog((context, configuration) => configuration
                               .UseDefaultSettings(context.Configuration, "Template project"));
                       });
        }
    }
}