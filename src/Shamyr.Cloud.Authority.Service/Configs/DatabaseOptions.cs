using System.Collections.Generic;
using System.Reflection;
using Shamyr.Cloud.Database;
using Shamyr.Extensions.MongoDB;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class DatabaseOptions: IDatabaseOptions
  {
    public List<Assembly> MigrationAssemblies => new List<Assembly> { typeof(EnvVariables).Assembly };
    public List<Assembly> DatabaseAssemblies => new List<Assembly> { typeof(DbCollections).Assembly };
    public bool MapDiscriminators => true;
    public int MetadataVersion => 2;
  }
}
