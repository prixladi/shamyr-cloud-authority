using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.CurrentUser
{
  public record PostPasswordModel
  {
    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string Password { get; init; } = default!;
  }
}
