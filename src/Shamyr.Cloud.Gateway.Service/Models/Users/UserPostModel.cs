using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  /// <summary>
  /// Model for creating new user
  /// </summary>
  public class UserPostModel
  {
    /// <summary>
    /// Unique username
    /// </summary>
    [StringLength(maximumLength: 500, MinimumLength = 6)]
    [Required]
    public string Username { get; set; } = default!;

    /// <summary>
    /// Password
    /// </summary>
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = default!;

    /// <summary>
    /// Unique email
    /// </summary>
    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;
  }
}
