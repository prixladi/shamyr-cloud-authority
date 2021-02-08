using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Shamyr.MongoDB.Configs;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public MongoClientSettings Settings
    {
      get
      {
        var settings = MongoClientSettings.FromConnectionString(EnvVariable.Get(EnvVariables._MongoUrl));

#if DEBUG
        settings.ClusterConfigurator = cb => {
          cb.Subscribe<CommandStartedEvent>(e => {
            Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
          });
        };
#endif

        return settings;
      }
    }

    public string DatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);
  }
}
