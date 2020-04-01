using System;

namespace Shamyr.Server.Database.Documents
{
  public class UserTokenDoc
  {
    public string Value { get; set; } = default!;

    public DateTime ExpirationUtc { get; set; }
  }
}
