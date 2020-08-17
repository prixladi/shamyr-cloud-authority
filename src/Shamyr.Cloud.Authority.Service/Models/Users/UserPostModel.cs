using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public class UserPostModel
  {
    [StringLength(maximumLength: ModelConstants._MaxUsernameLength, MinimumLength = ModelConstants._MinUsernameLength)]
    [Required]
    public string Username { get; set; } = default!;

    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string Password { get; set; } = default!;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;
  }
}
