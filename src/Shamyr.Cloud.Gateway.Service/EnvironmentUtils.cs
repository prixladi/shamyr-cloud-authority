﻿using System;

namespace Shamyr.Cloud.Gateway.Service
{
  public static class EnvironmentUtils
  {
    public static Uri PortalUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._PortalUrl));
    public static Uri GatewayUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._GatewayUrl));

    public static string MongoUrl { get; } = EnvVariable.Get(EnvVariables._MongoUrl);
    public static string MongoDatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);

    public static string BearerTokenIssuer { get; } = EnvVariable.Get(EnvVariables._BearerTokenIssuer);
    public static string BearerTokenAudience { get; } = EnvVariable.Get(EnvVariables._BearerTokenAudience);
    public static int RefreshTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._RefreshTokenDuration));
    public static int BearerTokenDuration { get; } = int.Parse(EnvVariable.Get(EnvVariables._BearerTokenDuration));
    public static string BearerTokenSymetricKeey { get; } = EnvVariable.Get(EnvVariables._BearerTokenSymetricKeey);

    public static string EmailHost { get; } = EnvVariable.Get(EnvVariables._EmailHost);
    public static int EmailPort { get; } = int.Parse(EnvVariable.Get(EnvVariables._EmailPort));
    public static bool EmailUseSsl { get; } = bool.Parse(EnvVariable.Get(EnvVariables._EmailUseSsl));
    public static string EmailSubjectSuffix { get; } = EnvVariable.Get(EnvVariables._EmailSubjectSuffix);
    public static string EmailSenderAlias { get; } = EnvVariable.Get(EnvVariables._EmailSenderAlias);
    public static string EmailSenderAddress { get; } = EnvVariable.Get(EnvVariables._EmailSenderAddress);
    public static string EmailSenderAddressPassword { get; } = EnvVariable.Get(EnvVariables._EmailSenderAddressPassword);
  }
}