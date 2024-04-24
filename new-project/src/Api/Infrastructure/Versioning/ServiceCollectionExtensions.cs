namespace Byndyusoft.Template.Api.Infrastructure.Versioning
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
                           {
                               options.ReportApiVersions = true;
                               options.DefaultApiVersion = ApiVersion.Default;
                               options.AssumeDefaultVersionWhenUnspecified = true;
                           })
                           .AddMvc()
                           .AddApiExplorer(options =>
                           {
                               options.GroupNameFormat = "'v'VVV";
                               options.SubstituteApiVersionInUrl = true;
                           })
                           .Services;
        }
    }
}