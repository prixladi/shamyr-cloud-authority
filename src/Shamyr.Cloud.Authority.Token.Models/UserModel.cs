namespace Shamyr.Cloud.Authority.Token.Models
{
  public class UserModel
  {
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Admin { get; set; } 
  }
}
