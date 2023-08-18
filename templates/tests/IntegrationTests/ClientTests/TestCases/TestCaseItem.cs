namespace Byndyusoft.Template.IntegrationTests.ClientTests.TestCases;

using DotNet.Testing.Infrastructure.TestBase;

public class TestCaseItem : TestCaseItemBase
{
    /// <summary>
    ///     Название папки, где находится файл для обработки
    /// </summary>
    public string TestCaseFolder { get; set; }

    /// <summary>
    ///     Входные параметры
    /// </summary>
    public TestCaseParameters Parameters { get; set; }

    /// <summary>
    ///     Настройки моков для тестов
    /// </summary>
    public TestCaseMocks Mocks { get; set; }

    /// <summary>
    ///     Объект с ожидаемыми значениями, которые проверяются в тест-кейсе
    /// </summary>
    public TestCaseExpectations Expected { get; set; }
}