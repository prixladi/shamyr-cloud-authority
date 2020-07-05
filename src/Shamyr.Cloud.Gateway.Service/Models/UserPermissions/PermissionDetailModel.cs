using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Models.UserPermissions
{
  /// <summary>
  /// Permission model
  /// </summary>
  public class PermissionDetailModel
  {
    /// <summary>
    /// Permission kind or <c>null</c>
    /// </summary>
    public PermissionKind? Kind { get; set; }
  }
}
