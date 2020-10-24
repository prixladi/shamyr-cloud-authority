using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class GetRequest: IRequest<DetailModel>
  {
    public ObjectId TemplateId { get; }

    public GetRequest(ObjectId templateId)
    {
      TemplateId = templateId;
    }
  }
}
