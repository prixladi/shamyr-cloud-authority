using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class PatchRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public PatchModel Model { get; }

    public PatchRequest(ObjectId templateId, PatchModel model)
    {
      TemplateId = templateId;
      Model = model;
    }
  }
}
