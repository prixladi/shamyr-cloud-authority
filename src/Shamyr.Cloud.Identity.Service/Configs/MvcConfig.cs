using Microsoft.AspNetCore.Mvc;
using Shamyr.AspNetCore.Attributes;

namespace Shamyr.Cloud.Identity.Service.Configs
{
  internal static class MvcConfig
  {
    public static void SetupMvcOptions(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
    }
  }
}