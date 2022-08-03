namespace Byndyusoft.Template.IntegrationTests.ClientTests
{
    using System.Threading.Tasks;
    using Api;
    using Api.Client.Clients;
    using Api.Client.Settings;
    using Fixtures;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Options;
    using OpenTracing.Mock;
    using Xunit;

    public class TemplateClientTests : IClassFixture<TestServiceProvider>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TemplateClient _templateClient;

        public TemplateClientTests(WebApplicationFactory<Program> factory, TestServiceProvider testServiceProvider)
        {
            var apiSettings = Options.Create(new TemplateApiSettings());

            _templateClient = new TemplateClient(factory.CreateClient(), new MockTracer(), apiSettings);
        }

        [Fact]
        public async Task GetId_AnyValue_ReturnsSameValue()
        {
            //Arrange
            var templateId = 1;

            //Act
            var templateDto = await _templateClient.GetTemplate(templateId);

            //Assert
            templateDto.Should().NotBeNull();
            templateDto.Id.Should().Be(templateId);
        }
    }
}