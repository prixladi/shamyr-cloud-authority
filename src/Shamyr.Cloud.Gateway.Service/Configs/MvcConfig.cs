using Microsoft.AspNetCore.Mvc;
using Shamyr.AspNetCore.Attributes;
using Shamyr.Cloud.Bson;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class MvcConfig
  {
    public static void SetupMvcOptions(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
      options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
    }

    public static void SetupJsonOptions(JsonOptions options)
    {
      options.JsonSerializerOptions.Converters.Add(new ObjectIdJsonConverter());
    }
  }
}
