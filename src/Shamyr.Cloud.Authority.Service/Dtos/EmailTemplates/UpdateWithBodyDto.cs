using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates
{
  public record UpdateWithBodyDto(
    string Name,
    string Subject,
    bool IsHtml,
    EmailTemplateType Type,
    string Body);
}
