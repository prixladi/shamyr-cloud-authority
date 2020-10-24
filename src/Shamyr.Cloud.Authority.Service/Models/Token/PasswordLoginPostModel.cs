using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Token
{
  public record PasswordLoginPostModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; init; } = default!;

    [Required]
    public string Password { get; init; } = default!;
  }
}
