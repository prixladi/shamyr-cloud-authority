using System;

namespace Shamyr.Cloud.Authority.Service
{
  public static class EnvironmentUtils
  {
    public static Uri PortalUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._PortalUrl));
    public static Uri AuthorityUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._AuthorityUrl));

    public static string MongoUrl { get; } = EnvVariable.Get(EnvVariables._MongoUrl);
    public static string MongoDatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);

    public static string BearerTokenIssuer { get; } = EnvVariable.Get(EnvVariables._BearerTokenIssuer);
    public static string BearerTokenAudience { get; } = EnvVariable.Get(EnvVariables._BearerTokenAudience);
    public static int RefreshTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._RefreshTokenDuration));
    public static int BearerTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._BearerTokenDuration));
    public static string BearerPrivateKey { get; } = EnvVariable.Get(EnvVariables._BearerPrivateKey);
    public static string BearerPublicKey { get; } = EnvVariable.Get(EnvVariables._BearerPublicKey);

    public static Uri EmailServerUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._EmailServerUrl));
    public static string EmailSenderAddress { get; } = EnvVariable.Get(EnvVariables._EmailSenderAddress);
  }
}
