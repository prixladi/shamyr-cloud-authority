using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class EmailTemplatePreviewDtoExtensions
  {
    public static PreviewModel ToPreview(this PreviewDto dto)
    {
      if (dto is null)
        throw new ArgumentNullException(nameof(dto));

      return new PreviewModel
      {
        Id = dto.Id,
        Name = dto.Name,
        Subject = dto.Subject,
        IsHtml = dto.IsHtml,
        Type = dto.Type
      };
    }

    public static ICollection<PreviewModel> ToPreview(this IEnumerable<PreviewDto> dtos)
    {
      if (dtos is null)
        throw new ArgumentNullException(nameof(dtos));

      return dtos.Select(ToPreview).ToList();
    }
  }
}
