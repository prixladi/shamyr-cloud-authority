using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Token
{
  public record RefreshLoginPostModel
  {
    [Required]
    public string RefreshToken { get; init; } = default!;
  }
}
