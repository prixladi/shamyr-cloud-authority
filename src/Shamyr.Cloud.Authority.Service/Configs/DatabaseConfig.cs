using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Shamyr.Logging;
using Shamyr.MongoDB.Configs;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    private readonly ILogger fLogger;

    public DatabaseConfig(ILogger logger)
    {
      fLogger = logger;
    }

    public MongoClientSettings Settings
    {
      get
      {
        var settings = MongoClientSettings.FromConnectionString(EnvVariable.Get(EnvVariables._MongoUrl));

        settings.ClusterConfigurator = cb =>
        {
          cb.Subscribe<CommandStartedEvent>(e =>
          {
            var command = $"Mongo command: {e.CommandName} - {e.Command.ToJson()}"
              .Replace("{", "[")
              .Replace("}", "]");

            fLogger.LogDebug(LoggingContext.Root, command);
          });
        };

        return settings;
      }
    }

    public string DatabaseName { get; } = EnvVariable.Get(EnvVariables._MongoDatabaseName);
  }
}
