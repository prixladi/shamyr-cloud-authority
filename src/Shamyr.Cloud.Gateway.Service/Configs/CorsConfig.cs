using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class CorsConfig
  {
    public static void Setup(CorsPolicyBuilder builder)
    {
      builder.AllowAnyHeader();
      builder.AllowAnyMethod();
      builder.AllowAnyOrigin();
    }
  }
}
