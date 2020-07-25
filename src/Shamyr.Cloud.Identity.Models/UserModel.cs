namespace Shamyr.Cloud.Identity.Service.Models
{
  public class UserModel
  {
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Disabled { get; set; }
    public bool Admin { get; set; } 
    public bool Verified { get; set; }
  }
}
