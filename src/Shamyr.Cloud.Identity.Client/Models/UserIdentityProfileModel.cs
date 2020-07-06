using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Identity.Client.Models
{
  public class UserIdentityProfileModel
  {
    [Required]
    public string Id { get; set; } = default!;

    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public bool Disabled { get; set; }

    [Required]
    public bool Admin { get; set; } = default!;
  }
}
