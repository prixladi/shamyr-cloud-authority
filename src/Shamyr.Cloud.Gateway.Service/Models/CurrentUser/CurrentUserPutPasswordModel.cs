using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.CurrentUser
{
  public class CurrentUserPutPasswordModel
  {
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string OldPassword { get; set; } = default!;

    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string NewPassword { get; set; } = default!;
  }
}
