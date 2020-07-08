using System;
using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class PatchSubjectRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public string Subject { get; }

    public PatchSubjectRequest(ObjectId templateId, string subject)
    {
      TemplateId = templateId;
      Subject = subject ?? throw new ArgumentNullException(nameof(subject));
    }
  }
}
