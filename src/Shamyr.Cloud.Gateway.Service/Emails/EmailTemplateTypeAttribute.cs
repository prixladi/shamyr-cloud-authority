using System;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public sealed class EmailTemplateTypeAttribute: Attribute
  {
    public EmailTemplateType Type { get; }

    public EmailTemplateTypeAttribute(EmailTemplateType type)
    {
      Type = type;
    }
  }
}
