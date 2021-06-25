namespace Byndyusoft.Template.Api.Client.Clients
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.Extensions.Options;
    using OpenTracing;
    using OpenTracing.Tag;
    using Settings;
    using Shared.Dtos;

    /// <inheritdoc cref="ITemplateClient" />
    public class TemplateClient : HttpServiceClient, ITemplateClient
    {
        private readonly ITracer _tracer;

        public TemplateClient(HttpClient httpClient,
            IOptions<TemplateApiSettings> apiSettings,
            ITracer tracer)
            : base(httpClient, apiSettings)
        {
            _tracer = tracer;
        }

        public async Task<TemplateDto> GetTemplate(int templateId)
        {
            using var scope = _tracer.BuildSpan(nameof(GetTemplate)).StartActive(true);

            try
            {
                var templateDto = await GetAsync<TemplateDto>($"/v1.0/templates/{templateId}").ConfigureAwait(false);

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