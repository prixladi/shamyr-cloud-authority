using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.ConnectToken
{
  public class GoogleLoginPostModel
  {
    [Required]
    public string IdToken { get; set; } = default!;
  }
}
