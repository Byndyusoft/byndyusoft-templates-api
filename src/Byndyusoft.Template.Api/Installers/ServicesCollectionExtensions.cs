namespace Byndyusoft.Template.Api.Installers
{
    using Domain.Services;
    using Domain.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITemplateService, TemplateService>();

            return serviceCollection;
        }
    }
}