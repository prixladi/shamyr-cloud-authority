namespace Shamyr.Cloud.Authority.Service
{
  public static class EnvVariables
  {
    public const string _AppInsightsKey = "APP_INSIGHTS_KEY";

    public const string _MongoUrl = "MONGO_URL";
    public const string _MongoDatabaseName = "MONGO_DATABASE_NAME";

    public const string _BearerTokenIssuer = "BEARER_TOKEN_ISSUER";
    public const string _BearerTokenAudience = "BEARER_TOKEN_AUDIENCE";
    public const string _RefreshTokenDuration = "REFRESH_TOKEN_DURATION";
    public const string _BearerTokenDuration = "BEARER_TOKEN_DURATION";
    public const string _BearerPrivateKey = "BEARER_PRIVATE_KEY";
    public const string _BearerPublicKey = "BEARER_PUBLIC_KEY";

    public const string _EmailServerUrl = "EMAIL_SERVER_URL";
    public const string _EmailSenderAddress = "EMAIL_SENDER_ADDRESS";
  }
}
