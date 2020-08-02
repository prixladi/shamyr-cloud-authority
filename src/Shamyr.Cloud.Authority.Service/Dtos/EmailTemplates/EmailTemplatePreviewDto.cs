using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates
{
  public class EmailTemplatePreviewDto
  {
    public ObjectId Id { get; set; }
    public string Name { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public bool IsHtml { get; set; }
    public EmailTemplateType Type { get; set; }
  }
}
