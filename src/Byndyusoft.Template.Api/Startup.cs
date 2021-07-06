namespace Byndyusoft.Template.Api
{
    using System.Text.Json.Serialization;
    using Extensions;
    using Installers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Npgsql;
    using Prometheus;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using Tracing;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRelationalDb(NpgsqlFactory.Instance, Configuration.GetConnectionString("Main"));
            
            services.AddVersioning();

            services.AddHealthChecks();

            services.AddSwagger();

            services.AddApplicationServices();

            services.AddControllers(options =>
                    {
                        options.PassRequestsToTracer()
                               .PassResponsesToTracer();
                    })
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsProduction() == false)
            {
                app.UseSwagger()
                   .UseSwaggerUI(options =>
                   {
                       foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                           options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", apiVersionDescription.GroupName.ToUpperInvariant());

                       options.DisplayRequestDuration();
                       options.DefaultModelRendering(ModelRendering.Model);
                       options.DefaultModelExpandDepth(3);
                   });
            }

            app
                .UseHealthChecks("/healthz")
                .UseMetricServer()
                .UseRouting()
                .UseRequestsMetrics()
                .UseEndpoints(endpoints => endpoints.MapControllers());

        }
    }
}