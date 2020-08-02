using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Extensions
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
  }
}
