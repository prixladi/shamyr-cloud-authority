using System;

namespace Shamyr.Cloud.Database.Documents
{
  public class TokenDoc
  {
    public string Value { get; set; } = default!;

    public DateTime ExpirationUtc { get; set; }
  }
}
