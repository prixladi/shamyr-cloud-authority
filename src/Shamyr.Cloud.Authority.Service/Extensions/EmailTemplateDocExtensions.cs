using System;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class EmailTemplateDocExtensions
  {
    public static DetailModel ToDetail(this EmailTemplateDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new DetailModel
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
