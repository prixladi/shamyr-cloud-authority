using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates
{
  public record PreviewDto(
    ObjectId Id,
    string Name,
    string Subject,
    bool IsHtml,
    EmailTemplateType Type);
}
