namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class JwtConfig: IJwtConfig
  {
    public string BearerTokenIssuer { get; } = EnvVariable.Get(EnvVariables._BearerTokenIssuer);
    public string BearerTokenAudience { get; } = EnvVariable.Get(EnvVariables._BearerTokenAudience);
    public int RefreshTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._RefreshTokenDuration));
    public int BearerTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._BearerTokenDuration));
    public string BearerPrivateKey { get; } = EnvVariable.Get(EnvVariables._BearerPrivateKey);
    public string BearerPublicKey { get; } = EnvVariable.Get(EnvVariables._BearerPublicKey);
  }
}
