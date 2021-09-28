namespace Byndyusoft.Template.Api.Controllers
{
    using Domain.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Contracts.TemplateEntity;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplatesController(ITemplateService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<TemplateDto> GetTemplate(int id)
        {
            var resultId = _service.GetId(id);

            return new TemplateDto {Id = resultId};
        }
    }
}