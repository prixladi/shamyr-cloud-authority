using System;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class UserQueryFilterExtensions
  {
    public static FilterDto ToDto(this QueryFilter filter)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      return new FilterDto(filter.TemplateType);
    }
  }
}
