namespace Shamyr.Cloud.Authority.Models
{
  public record TokenConfigurationModel
  {
    public string PublicKey { get; init; } = default!;
    public int KeyDuration { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SignatureAlgorithm { get; init; } = default!;
  }
}
