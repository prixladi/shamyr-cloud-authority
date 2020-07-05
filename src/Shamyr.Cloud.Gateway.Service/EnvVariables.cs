namespace Shamyr.Cloud.Gateway.Service
{
  public static class EnvVariables
  {
    public const string _AppInsightsKey = "APP_INSIGHTS_KEY";

    public const string _MongoUrl = "MONGO_URL";
    public const string _MongoDatabaseName = "MONGO_DATABASE_NAME";

    public const string _GatewayUrl = "GATEWAY_URL";
    public const string _PortalUrl = "PORTAL_URL";

    public const string _BearerTokenIssuer = "BEARER_TOKEN_ISSUER";
    public const string _BearerTokenAudience = "BEARER_TOKEN_AUDIENCE";
    public const string _RefreshTokenDuration = "REFRESH_TOKEN_DURATION";
    public const string _BearerTokenDuration = "BEARER_TOKEN_DURATION";
    public const string _BearerTokenSymetricKeey = "BEARER_TOKEN_SYMETRIC_KEY";

    public const string _EmailHost = "EMAIL_HOST";
    public const string _EmailPort = "EMAIL_PORT";
    public const string _EmailUseSsl = "EMAIL_USE_SSL";
    public const string _EmailSubjectSuffix = "EMAIL_SUBJECT_SUFFIX";
    public const string _EmailSenderAlias = "EMAIL_SENDER_ALIAS";
    public const string _EmailSenderAddress = "EMAIL_SENDER_ADDRESS";
    public const string _EmailSenderAddressPassword = "EMAIL_SENDER_ADDRESS_PASSWORD";
  }
}
