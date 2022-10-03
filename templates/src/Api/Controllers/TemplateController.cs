namespace Byndyusoft.Template.Api.Controllers
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mime;
    using Contracts.TemplateEntity;
    using Domain.Services.Interfaces;
    using Infrastructure.OpenTelemetry;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Templates API implementation
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _service;

        /// <summary>
        ///     Initializes controller dependencies
        /// </summary>
        /// <param name="service">templates service</param>
        public TemplatesController(ITemplateService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Returns template dto
        /// </summary>
        /// <param name="id">Template dto id</param>
        /// <returns>Template dto</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TemplateDto))]
        public ActionResult GetTemplate(int id)
        {
            var stopwatch = Stopwatch.StartNew();

            var resultId = _service.GetId(id);

            stopwatch.Stop();
            ApiTemplateMetrics.ObserveDuration(id, stopwatch.Elapsed);

            return resultId.HasValue
                       ? Ok(new TemplateDto { Id = resultId.Value })
                       : NotFound();
        }
    }
}