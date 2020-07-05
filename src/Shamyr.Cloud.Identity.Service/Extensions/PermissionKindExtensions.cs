using static Shamyr.Cloud.Identity.Service.Protos.UserIdentityProfileMessage.Types;

namespace Shamyr.Cloud.Identity.Service.Extensions
{
  public static class PermissionKindExtensions
  {
    public static PermissionKind ToMessageKind(this Database.PermissionKind? kind)
    {
      return kind switch
      {
        Database.PermissionKind.View => PermissionKind.View,
        Database.PermissionKind.Control => PermissionKind.Control,
        Database.PermissionKind.Configure => PermissionKind.Configure,
        Database.PermissionKind.Own => PermissionKind.Own,
        _ => PermissionKind.None
      };
    }
  }
}
