namespace Byndyusoft.Template.Api.Contracts.TemplateEntity
{
    using System.Threading.Tasks;

    public interface ITemplateClient
    {
        Task<TemplateDto> GetTemplate(int templateId);
    }
}