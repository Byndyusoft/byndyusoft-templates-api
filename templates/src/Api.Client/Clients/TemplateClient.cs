namespace Byndyusoft.Template.Api.Client.Clients
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Byndyusoft.ApiClient;
    using Microsoft.Extensions.Options;
    using Settings;
    using Contracts.TemplateEntity;

    /// <inheritdoc cref="ITemplateClient" />
    public class TemplateClient : BaseClient, ITemplateClient
    {
        private const string ApiPrefix = "api/v1/templates";

        private readonly ActivitySource _myActivitySource;
        
        public TemplateClient(
            HttpClient httpClient,
            IOptions<TemplateApiSettings> apiSettings,
            IOptions<OpenTelemetrySettings> openTelemetrySettings
        ) : base(httpClient, apiSettings)
        {
            _myActivitySource = new ActivitySource(openTelemetrySettings.Value.SourceName); 
        }

        public async Task<TemplateDto> GetTemplate(int templateId, CancellationToken cancellationToken)
        {
            using var activitySource = _myActivitySource.StartActivity();
            
            try
            {
                var templateDto = await GetAsync<TemplateDto>($"{ApiPrefix}/{templateId}", cancellationToken);
                return templateDto;
            }
            catch (Exception)
            {
                activitySource?.SetTag("Error", true);
                throw;
            }
        }
    }

}