using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class EmailTemplatePostModelExtensions
  {
    public static EmailTemplateDoc ToDoc(this EmailTemplatePostModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new EmailTemplateDoc
      {
        Name = model.Name,
        Subject = model.Subject,
        Body = model.Body,
        IsHtml = model.IsHtml,
        Type = model.Type
      };
    }
  }
}
