namespace Byndyusoft.Template.Api.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
                           {
                               options.DefaultApiVersion = ApiVersion.Default;
                               options.AssumeDefaultVersionWhenUnspecified = true;
                               options.ReportApiVersions = true;
                           })
                           .AddVersionedApiExplorer(options =>
                           {
                               options.GroupNameFormat = "'v'VVV";
                               options.SubstituteApiVersionInUrl = true;
                           });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                        .AddSwaggerGen();
        }
    }
}