using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates
{
  public class EmailTemplateUpdateDto
  {
    public string Name { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool IsHtml { get; set; }
    public EmailTemplateType Type { get; set; }
  }
}
