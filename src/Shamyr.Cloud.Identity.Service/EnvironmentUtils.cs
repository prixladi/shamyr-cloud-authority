namespace Shamyr.Cloud.Identity.Service
{
  public static class EnvironmentUtils
  {
    public static string MongoUrl { get; } = EnvVariable.Get(EnvVariables._MongoUrl);
    public static string MongoDatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);
  }
}
