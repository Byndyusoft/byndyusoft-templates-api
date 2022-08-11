namespace UnitTests.Domain.Services;

using Byndyusoft.Template.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

public class TemplateServiceTests
{
    private readonly TemplateService _service;

    public TemplateServiceTests()
    {
        _service = new TemplateService(NullLogger<TemplateService>.Instance);
    }

    [Fact]
    public void GetId_AnyNumber_ReturnsSameValue()
    {
        // Arrange
        var id = 1;

        // Act
        var getResult = _service.GetId(id);

        // Assert
        getResult.Should().Be(1);
    }
}