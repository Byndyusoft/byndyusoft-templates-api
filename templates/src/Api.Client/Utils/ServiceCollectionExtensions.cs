namespace Byndyusoft.Template.Api.Client.Utils
{
    using Clients;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Settings;
    using Contracts.TemplateEntity;

    public static class ServiceCollectionExtension
    {
        /// <summary>
        ///     Для работы клиенты необходимо указать настройки подключения к апи в appsetting
        /// </summary>
        public static void AddTemplateClient(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions()
                .Configure<TemplateApiSettings>(configuration.GetSection(nameof(TemplateApiSettings)));

            services.AddHttpClient<ITemplateClient, TemplateClient>();
        }
    }
}