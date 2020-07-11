using System;
using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class PatchIsHtmlRequest: IRequest
  {
    public ObjectId TemplateId { get; }
    public bool IsHtml { get; }

    public PatchIsHtmlRequest(ObjectId templateId, bool isHtml)
    {
      TemplateId = templateId;
      IsHtml = isHtml;
    }
  }
}
