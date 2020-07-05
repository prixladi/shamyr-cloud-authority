using Microsoft.AspNetCore.Authorization;
using Shamyr.Cloud.Gateway.Service.Authorization;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class AuthorizationConfig
  {
    public static void Setup(AuthorizationOptions options)
    {
      options.AddPolicy(UserPolicy._View, builder => builder.RequireRole(UserPolicy.GetRequiredRoles(PermissionKind.View)));
      options.AddPolicy(UserPolicy._Control, builder => builder.RequireRole(UserPolicy.GetRequiredRoles(PermissionKind.Control)));
      options.AddPolicy(UserPolicy._Configure, builder => builder.RequireRole(UserPolicy.GetRequiredRoles(PermissionKind.Configure)));
      options.AddPolicy(UserPolicy._Own, builder => builder.RequireRole(UserPolicy.GetRequiredRoles(PermissionKind.Own)));
    }
  }
}
