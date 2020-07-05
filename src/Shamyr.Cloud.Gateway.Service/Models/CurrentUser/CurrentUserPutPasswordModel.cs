using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.CurrentUser
{
  public class CurrentUserPutPasswordModel
  {
    [Required]
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string OldPassword { get; set; } = default!;

    [Required]
    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string NewPassword { get; set; } = default!;
  }
}
