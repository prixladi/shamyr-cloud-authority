using System;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class EmailTemplatePutModelExtensions
  {
    public static UpdateWithBodyDto ToDto(this PutModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new UpdateWithBodyDto(
        Name: model.Name,
        Subject: model.Subject,
        Body: model.Body,
        IsHtml: model.IsHtml,
        Type: model.Type);
    }

    public static UpdateDto ToDto(this PatchModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new UpdateDto(
        Name: model.Name,
        Subject: model.Subject,
        IsHtml: model.IsHtml,
        Type: model.Type);
    }
  }
}
