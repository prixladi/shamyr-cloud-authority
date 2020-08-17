using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.CurrentUser
{
  public class CurrentUserPutPasswordModel
  {
    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string OldPassword { get; set; } = default!;

    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string NewPassword { get; set; } = default!;
  }
}
