using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public class UserPatchPasswordModel
  {
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string PasswordToken { get; set; } = default!;
  }
}
