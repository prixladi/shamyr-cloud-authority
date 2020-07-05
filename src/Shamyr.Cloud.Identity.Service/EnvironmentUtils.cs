namespace Shamyr.Cloud.Identity.Service
{
  public static class EnvironmentUtils
  {
    public static string MongoUrl { get; } = EnvVariable.Get(EnvVariables._MongoUrl);
    public static string MongoDatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);

    public static string BearerTokenIssuer { get; } = EnvVariable.Get(EnvVariables._BearerTokenIssuer);
    public static string BearerTokenAudience { get; } = EnvVariable.Get(EnvVariables._BearerTokenAudience);
    public static string BearerTokenSymetricKey { get; } = EnvVariable.Get(EnvVariables._BearerTokenSymetricKey);
  }
}
