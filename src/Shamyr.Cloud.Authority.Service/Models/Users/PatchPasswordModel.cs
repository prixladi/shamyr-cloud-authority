using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record PatchPasswordModel
  {
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string Password { get; init; } = default!;

    [Required]
    public string PasswordToken { get; init; } = default!;
  }
}
