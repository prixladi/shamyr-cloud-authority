using System;
using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class PatchNameRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public string Name { get; }

    public PatchNameRequest(ObjectId templateId, string name)
    {
      TemplateId = templateId;
      Name = name ?? throw new ArgumentNullException(nameof(name));
    }
  }
}
