using System;
using Shamyr.Cloud.Authority.Service.Dtos.Users;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class EmailTemplateQueryFilterExtensions
  {
    public static FilterDto ToDto(this QueryFilter filter)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      return new FilterDto(
        Skip: filter.Skip,
        Take: filter.Take,
        Email: filter.Email,
        Admin: filter.Admin,
        Username: filter.Username
      );
    }
  }
}
