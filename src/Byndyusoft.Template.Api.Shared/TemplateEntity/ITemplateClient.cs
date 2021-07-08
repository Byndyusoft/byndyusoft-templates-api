namespace Byndyusoft.Template.Api.Shared.TemplateEntity
{
    using System.Threading.Tasks;

    public interface ITemplateClient
    {
        Task<TemplateDto> GetTemplate(int templateId);
    }
}