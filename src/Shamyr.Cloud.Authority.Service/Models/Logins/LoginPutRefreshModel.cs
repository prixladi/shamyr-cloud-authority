using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Logins
{
  public class LoginPutRefreshModel
  {
    [Required]
    public string RefreshToken { get; set; } = default!;

    [Required]
    public string BearerToken { get; set; } = default!;
  }
}
