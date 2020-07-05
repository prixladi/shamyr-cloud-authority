namespace Shamyr.Cloud.Identity.Service.Configs
{
  public interface IJwtConfig
  {
    string BearerTokenAudience { get; }
    string BearerTokenIssuer { get; }
    string BearerTokenSymetricKey { get; }
  }
}