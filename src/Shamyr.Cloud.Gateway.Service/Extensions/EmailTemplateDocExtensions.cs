using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class EmailTemplateDocExtensions
  {
    public static EmailTemplateDetailModel ToDetail(this EmailTemplateDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new EmailTemplateDetailModel
      {
        Id = doc.Id,
        Name = doc.Name,
        Subject = doc.Subject,
        Body = doc.Body,
        IsHtml = doc.IsHtml,
        Type = doc.Type
      };
    }

    public static EmailTemplatePreviewDto ToPreviewDto(this EmailTemplateDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new EmailTemplatePreviewDto
      {
        Id = doc.Id,
        Name = doc.Name,
        Subject = doc.Subject,
        IsHtml = doc.IsHtml,
        Type = doc.Type
      };
    }

    public static ICollection<EmailTemplatePreviewDto> ToPreviewDto(this IEnumerable<EmailTemplateDoc> docs)
    {
      if (docs is null)
        throw new ArgumentNullException(nameof(docs));

      return docs.Select(ToPreviewDto).ToList();
    }
  }
}
