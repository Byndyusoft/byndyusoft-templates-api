namespace Byndyusoft.Template.Api.Infrastructure.Swagger
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var apiVersionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
                options.SwaggerDoc(apiVersionDescription.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersionDescription.ApiVersion.ToString(),
                        Description = apiVersionDescription.IsDeprecated ? "DEPRECATED" : "",
                        Title = Assembly.GetExecutingAssembly().GetName().Name
                    });


            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");

            foreach (var xmlFile in xmlFiles)
                options.IncludeXmlComments(xmlFile);
        }
    }
}