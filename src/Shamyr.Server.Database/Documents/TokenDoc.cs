using System;

namespace Shamyr.Server.Database.Documents
{
  public class TokenDoc
  {
    public string Value { get; set; } = default!;

    public DateTime ExpirationUtc { get; set; }
  }
}
