using Shamyr.MongoDB.Configs;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public string DatabaseUrl { get; } = EnvVariable.Get(EnvVariables._MongoUrl);
    public string DatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);
  }
}
