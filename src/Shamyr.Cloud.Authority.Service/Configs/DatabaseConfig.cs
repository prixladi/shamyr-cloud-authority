using Shamyr.Cloud.Database;
using Shamyr.Extensions.MongoDB;
using Shamyr.MongoDB.Configs;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public string DatabaseUrl => EnvironmentUtils.MongoUrl;
    public string DatabaseName => EnvironmentUtils.MongoDatabaseName;

    public static void Setup(DatabaseOptions options)
    {
      options.MetadataVersion = 2;
      options.MigrationAssemblies.Add(typeof(EnvVariables).Assembly);
      options.DatabaseAssemblies.Add(typeof(DbCollections).Assembly);
      options.MapDiscriminators = true;
    }
  }
}
