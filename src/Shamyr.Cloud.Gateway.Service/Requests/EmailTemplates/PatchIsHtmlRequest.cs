using System;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class PatchIsHtmlRequest
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
