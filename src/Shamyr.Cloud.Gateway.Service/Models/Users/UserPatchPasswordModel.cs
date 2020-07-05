using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  /// <summary>
  /// New user password model
  /// </summary>
  public class UserPatchPasswordModel
  {
    /// <summary>
    /// New password
    /// </summary>
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string PasswordToken { get; set; } = default!;
  }
}
