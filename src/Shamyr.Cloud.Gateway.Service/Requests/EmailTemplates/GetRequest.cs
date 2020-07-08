using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class GetRequest: IRequest<EmailTemplateDetailModel>
  {
    public ObjectId TemplateId { get; }

    public GetRequest(ObjectId templateId)
    {
      TemplateId = templateId;
    }
  }
}
