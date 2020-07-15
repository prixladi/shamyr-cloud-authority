using System;
using Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class EmailTemplatePutModelExtensions
  {
    public static EmailTemplateUpdateDto ToDto(this EmailTemplatePutModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new EmailTemplateUpdateDto
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
