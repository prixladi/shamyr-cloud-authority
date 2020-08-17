using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.CurrentUser
{
  public class CurrentUserPostPasswordModel
  {
    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string Password { get; set; } = default!;
  }
}
