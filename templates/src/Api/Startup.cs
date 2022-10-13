namespace Byndyusoft.Template.Api
{
    using System.Text.Json.Serialization;
    using Infrastructure.Swagger;
    using Infrastructure.Versioning;
    using Installers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Npgsql;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddTracing();

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddHealthChecks();

            services
                .AddVersioning()
                .AddSwagger();

            services
                .AddRelationalDb(NpgsqlFactory.Instance, Configuration.GetConnectionString("Main"))
                .AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider
        )
        {
            if (env.IsProduction() == false)
                app.UseSwagger(apiVersionDescriptionProvider);

            app
                .UseHealthChecks("/healthz")
                .UseOpenTelemetryPrometheusScrapingEndpoint()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}