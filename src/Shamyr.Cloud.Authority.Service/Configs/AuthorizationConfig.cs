using Microsoft.AspNetCore.Authorization;
using Shamyr.Cloud.Authority.Service.Authorization;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public static class AuthorizationConfig
  {
    public static void Setup(AuthorizationOptions options)
    {
      options.AddPolicy(UserPolicy._Admin, builder => builder.RequireRole(UserPolicy._Admin));
    }
  }
}
