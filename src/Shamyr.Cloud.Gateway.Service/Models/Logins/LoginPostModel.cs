using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Logins
{
  public class LoginPostModel
  {
    /// <summary>
    /// Username
    /// </summary>
    [Required]
    public string Username { get; set; } = default!;

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    public string Password { get; set; } = default!;
  }
}
