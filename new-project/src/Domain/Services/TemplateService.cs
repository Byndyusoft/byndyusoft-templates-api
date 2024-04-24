namespace Byndyusoft.Template.Domain.Services
{
    using Interfaces;
    using Microsoft.Extensions.Logging;

    public class TemplateService : ITemplateService
    {
        private readonly ILogger<TemplateService> _logger;

        public TemplateService(ILogger<TemplateService> logger)
        {
            _logger = logger;
        }

        public int? GetId(int id)
        {
            _logger.LogInformation("Get some id {Id}", id);

            return id;
        }
    }
}