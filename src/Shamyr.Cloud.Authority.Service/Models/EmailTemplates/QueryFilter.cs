using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Models.EmailTemplates
{
  public record QueryFilter
  {
    public EmailTemplateType? TemplateType { get; init; }
  }
}
