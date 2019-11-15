using System;

namespace Shamyr.Server.Database.Documents.Users
{
  public class UserToken
  {
    public string Value { get; set; } = default!;

    public DateTime ExpirationUtc { get; set; }
  }
}
