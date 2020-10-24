using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Token
{
  public record GoogleLoginPostModel
  {
    [Required]
    public string IdToken { get; init; } = default!;
  }
}
