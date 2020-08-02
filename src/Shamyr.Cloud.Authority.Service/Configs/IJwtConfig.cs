namespace Shamyr.Cloud.Authority.Service.Configs
{
  public interface IJwtConfig
  {
    string BearerTokenIssuer { get; }
    string BearerTokenAudience { get; }
    int RefreshTokenDuration { get; }
    int BearerTokenDuration { get; }
    string BearerPrivateKey { get; }
    string BearerPublicKey { get; }
  }
}