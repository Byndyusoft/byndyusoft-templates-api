namespace Byndyusoft.Template.Api.Infrastructure.Swagger
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Swashbuckle.AspNetCore.SwaggerUI;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder builder,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            builder.UseSwagger()
                   .UseSwaggerUI(options =>
                   {
                       foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                           options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", apiVersionDescription.GroupName.ToUpperInvariant());

                       options.DisplayRequestDuration();
                       options.DefaultModelRendering(ModelRendering.Model);
                       options.DefaultModelExpandDepth(3);
                   });

            return builder;
        }
    }
}