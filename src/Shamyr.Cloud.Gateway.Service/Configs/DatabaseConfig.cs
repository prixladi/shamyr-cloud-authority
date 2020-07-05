using Shamyr.MongoDB.Configs;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public string DatabaseUrl => EnvironmentUtils.MongoUrl;
    public string DatabaseName => EnvironmentUtils.MongoDatabaseName;
  }
}
