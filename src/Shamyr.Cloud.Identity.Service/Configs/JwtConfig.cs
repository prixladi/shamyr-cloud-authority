namespace Shamyr.Cloud.Identity.Service.Configs
{
  public class JwtConfig: IJwtConfig
  {
    public string BearerTokenIssuer => EnvironmentUtils.BearerTokenIssuer;
    public string BearerTokenAudience => EnvironmentUtils.BearerTokenAudience;
    public string BearerTokenSymetricKey => EnvironmentUtils.BearerTokenSymetricKey;
  }
}
