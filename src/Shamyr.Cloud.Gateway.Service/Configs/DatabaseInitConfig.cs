using Shamyr.Cloud.Database;
using Shamyr.Extensions.MongoDB;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class DatabaseInitConfig
  {
    public static void Setup(DatabaseOptions options)
    {
      options.MetadataVersion = 2;
      options.MigrationAssemblies.Add(typeof(EnvVariables).Assembly);
      options.DatabaseAssemblies.Add(typeof(DbCollections).Assembly);
      options.MapDiscriminators = true;
    }
  }
}
