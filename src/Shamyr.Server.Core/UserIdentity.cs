using System;

namespace Shamyr.Server
{
  public class UserIdentity
  {
    public string Id { get; }
    public string Username { get; }
    public string Email { get; }

    public UserIdentity(string id, string username, string email)
    {
      Id = id ?? throw new ArgumentNullException(nameof(id));
      Username = username ?? throw new ArgumentNullException(nameof(username));
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }
  }
}
