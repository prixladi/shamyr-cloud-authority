using System.ComponentModel.DataAnnotations;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Models.UserPermissions
{
  /// <summary>
  /// Model for editing permission
  /// </summary>
  public class PermissionPatchModel
  {
    /// <summary>
    /// Permission kind
    /// </summary>
    [EnumDataType(typeof(PermissionKind))]
    [Required]
    public PermissionKind? Kind { get; set; }
  }
}
