using System;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class PutRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public EmailTemplatePutModel Model { get; }

    public PutRequest(ObjectId templateId, EmailTemplatePutModel model)
    {
      TemplateId = templateId;
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
