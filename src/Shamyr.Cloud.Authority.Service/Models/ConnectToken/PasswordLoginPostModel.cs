using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.ConnectToken
{
  public class PasswordLoginPostModel
  {
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
  }
}
