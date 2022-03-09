namespace Byndyusoft.Template.Api.Client.Clients
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Byndyusoft.ApiClient;
    using Microsoft.Extensions.Options;
    using OpenTracing;
    using OpenTracing.Tag;
    using Settings;
    using Contracts.TemplateEntity;

    /// <inheritdoc cref="ITemplateClient" />
    public class TemplateClient : BaseClient, ITemplateClient
    {
        private const string ApiPrefix = "api/v1/templates";
        private readonly ITracer _tracer;

        public TemplateClient(
            HttpClient httpClient,
            ITracer tracer,
            IOptions<TemplateApiSettings> apiSettings
        ) : base(httpClient, apiSettings)
        {
            _tracer = tracer;
        }

        public async Task<TemplateDto> GetTemplate(int templateId)
        {
            using var scope = _tracer.BuildSpan(nameof(GetTemplate)).StartActive(true);

            try
            {
                var templateDto = await GetAsync<TemplateDto>($"{ApiPrefix}/{templateId}");
                return templateDto;
            }
            catch (Exception)
            {
                scope.Span.SetTag(Tags.Error, true);
                throw;
            }
        }
    }

}