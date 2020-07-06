using Microsoft.AspNetCore.Authorization;
using Shamyr.Cloud.Gateway.Service.Authorization;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class AuthorizationConfig
  {
    public static void Setup(AuthorizationOptions options)
    {
      options.AddPolicy(UserPolicy._Admin, builder => builder.RequireRole(UserPolicy._Admin));
    }
  }
}
