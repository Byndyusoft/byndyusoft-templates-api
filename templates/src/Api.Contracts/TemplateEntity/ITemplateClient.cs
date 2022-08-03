namespace Byndyusoft.Template.Api.Contracts.TemplateEntity
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Интерфейс http клиента
    /// </summary>
    public interface ITemplateClient
    {
        /// <summary>
        ///     Получение dto
        /// </summary>
        /// <param name="templateId">ИД сущности</param>
        Task<TemplateDto> GetTemplate(int templateId);
    }
}