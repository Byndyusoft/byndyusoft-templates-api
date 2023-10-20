namespace Byndyusoft.Template.Api.Contracts.TemplateEntity
{
    using System.Threading;
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
        /// <param name="cancellationToken">Токен отмены</param>
        Task<TemplateDto> GetTemplate(int templateId, CancellationToken cancellationToken);
    }
}