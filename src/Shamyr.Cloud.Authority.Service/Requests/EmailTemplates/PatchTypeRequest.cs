using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class PatchTypeRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public EmailTemplateType Type { get; }

    public PatchTypeRequest(ObjectId templateId, EmailTemplateType type)
    {
      TemplateId = templateId;
      Type = type;
    }
  }
}
