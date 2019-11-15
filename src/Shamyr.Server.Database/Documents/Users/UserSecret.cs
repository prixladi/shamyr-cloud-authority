namespace Shamyr.Server.Database.Documents.Users
{
  public class UserSecret
  {
    public string PasswordHash { get; set; } = default!;
    public string Salt { get; set; } = default!;
  }
}
