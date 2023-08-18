namespace Byndyusoft.Template.IntegrationTests.ClientTests.TestCases
{
        using Api.Contracts.TemplateEntity;

        /// <summary>
        ///     Класс с необходимыми ожидаемыми результатами (строка в БД, сообщения в очередях, вызовы апи)    
        /// </summary>
        public class TestCaseExpectations
        {
                public TemplateDto? TemplateDto { get; set; }
        }
}