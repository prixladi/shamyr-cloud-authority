namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public class JwtConfig: IJwtConfig
  {
    public string BearerTokenIssuer => EnvironmentUtils.BearerTokenIssuer;
    public string BearerTokenAudience => EnvironmentUtils.BearerTokenAudience;
    public int RefreshTokenDuration => EnvironmentUtils.RefreshTokenDuration;
    public int BearerTokenDuration => EnvironmentUtils.BearerTokenDuration;
    public string BearerTokenSymetricKey => EnvironmentUtils.BearerTokenSymetricKeey;
  }
}
