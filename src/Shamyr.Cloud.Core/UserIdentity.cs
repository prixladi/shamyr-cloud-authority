using System;

namespace Shamyr.Cloud
{
  public class UserIdentity
  {
    public string Id { get; }
    public string? Username { get; }
    public string Email { get; }

    public UserIdentity(string id, string email, string? username)
    {
      Id = id ?? throw new ArgumentNullException(nameof(id));
      Email = email ?? throw new ArgumentNullException(nameof(email));
      Username = username;
    }
  }
}
