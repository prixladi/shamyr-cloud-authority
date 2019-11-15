namespace Shamyr.Server
{
  public interface IJwtConfig
  {
    string BearerTokenIssuer { get; }
    string BearerTokenAudience { get; }
    int RefreshTokenDuration { get; }
    int BearerTokenDuration { get; }
    string BearerTokenSymetricKey { get; }
  }
}
