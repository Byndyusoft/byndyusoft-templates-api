namespace Byndyusoft.Template.Api.Client.Interfaces
{
    using System.Threading.Tasks;
    using Shared.Dtos;

    public interface ITemplateClient
    {
        Task<TemplateDto> GetTemplate(int templateId);
    }
}