namespace Byndyusoft.Template.Api.Client.Clients
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Byndyusoft.ApiClient;
    using Microsoft.Extensions.Options;
    using Settings;
    using Contracts.TemplateEntity;

    /// <inheritdoc cref="ITemplateClient" />
    public class TemplateClient : BaseClient, ITemplateClient
    {
        private const string ApiPrefix = "api/v1/templates";

        public TemplateClient(
            HttpClient httpClient,
            IOptions<TemplateApiSettings> apiSettings
        ) : base(httpClient, apiSettings)
        {
        }

        public async Task<TemplateDto> GetTemplate(int templateId)
        {
            try
            {
                var templateDto = await GetAsync<TemplateDto>($"{ApiPrefix}/{templateId}");
                return templateDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}