using System;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class PutRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public PutModel Model { get; }

    public PutRequest(ObjectId templateId, PutModel model)
    {
      TemplateId = templateId;
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
