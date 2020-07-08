using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  public class UserPostModel
  {
    [StringLength(maximumLength: 500, MinimumLength = 6)]
    [Required]
    public string Username { get; set; } = default!;

    [StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = default!;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;
  }
}
