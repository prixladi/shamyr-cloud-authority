using System;
using System.Linq.Expressions;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Expressions
{
  public static class EmailTemplateDocExpression
  {
    public static Expression<Func<EmailTemplateDoc, EmailTemplatePreviewDto>> ToPreviewDto => doc => new EmailTemplatePreviewDto
    {
      Id = doc.Id,
      Name = doc.Name,
      Subject = doc.Subject,
      Type = doc.Type,
      IsHtml = doc.IsHtml
    };
  }
}
