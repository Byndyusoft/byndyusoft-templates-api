namespace Byndyusoft.Template.IntegrationTests.ClientTests
{
    using System.Reflection;
    using System.Threading.Tasks;
    using Api;
    using Api.Client.Clients;
    using Api.Client.Settings;
    using Api.Contracts.TemplateEntity;
    using DotNet.Testing.Infrastructure.ReadmeGeneration.Entities;
    using DotNet.Testing.Infrastructure.ReadmeGeneration.Services;
    using Fixtures;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Options;
    using OpenTracing.Mock;
    using TestCases;
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
            var testCase = new TestCaseItem
                               {
                                   TestId = "TemplateClient",
                                   Description = "TemplateClient",
                                   //TestCaseFolder = "TemplateClient",
                                   Parameters = new TestCaseParameters
                                                    {
                                                        TemplateId = 1
                                                    },
                                   Mocks = new TestCaseMocks(),
                                   Expected = new TestCaseExpectations
                                                  {
                                                      TemplateDto = new TemplateDto
                                                                        {
                                                                            Id = 1
                                                                        }
                                                  }
                               };

            //Act
            var templateDto = await _templateClient.GetTemplate(testCase.Parameters.TemplateId);

            //Assert
            templateDto.Should().NotBeNull();
            templateDto.Id.Should().Be(testCase.Expected.TemplateDto.Id);
        }
        
        [Fact]
        public async Task AddReport_FromCurrentDomain_ShouldAddReadmeInSolutionRoot()
        {
            // ARRANGE
            var reporter = TestCaseReadmeSolutionReporter.New();

            // ACT
            var reportConsistency = await reporter.AddReport(Assembly.GetExecutingAssembly());

            // ASSERT
            reportConsistency.Should().Be(ReportConsistency.Consistent);
        }
    }
}