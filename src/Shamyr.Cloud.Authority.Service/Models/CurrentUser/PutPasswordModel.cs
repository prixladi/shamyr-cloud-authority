using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.CurrentUser
{
  public record PutPasswordModel
  {
    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string OldPassword { get; init; } = default!;

    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string NewPassword { get; init; } = default!;
  }
}
