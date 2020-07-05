using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  /// <summary>
  /// Preview of user
  /// </summary>
  public class UserPreviewModel
  {
    /// <summary>
    /// UserDoc's id
    /// </summary>
    [Required]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    [Required]
    public string Username { get; set; } = default!;

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Indicates if user is disabled or not
    /// </summary>
    [Required]
    public bool Disabled { get; set; }

    [Required]
    public PermissionDetailModel UserPermissionDoc { get; set; } = default!;
  }
}
