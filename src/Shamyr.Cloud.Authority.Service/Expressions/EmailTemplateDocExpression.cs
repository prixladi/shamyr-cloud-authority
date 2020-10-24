using System;
using System.Linq.Expressions;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Expressions
{
  public static class EmailTemplateDocExpression
  {
    public static Expression<Func<EmailTemplateDoc, PreviewDto>> ToPreviewDto => doc => new PreviewDto
    (
      doc.Id,
      doc.Name,
      doc.Subject,
      doc.IsHtml,
      doc.Type
    );
  }
}
