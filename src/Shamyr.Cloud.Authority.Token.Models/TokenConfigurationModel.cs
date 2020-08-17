﻿namespace Shamyr.Cloud.Authority.Models
{
  public class TokenConfigurationModel
  {
    public string PublicKey { get; set; } = default!;
    public int KeyDuration { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string SignatureAlgorithm { get; set; } = default!;
  }
}
