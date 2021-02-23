using System;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Shamyr.Logging;

using static Shamyr.ExtensibleLogging.Constants;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public static class LoggingConfig
  {
    public static void Setup(WebHostBuilderContext _, LoggerConfiguration config)
    {
      LogEventLevel logLevel = LogEventLevel.Warning;
      if (Enum.TryParse(EnvVariable.TryGet(EnvVariables._LogLevel), true, out LogEventLevel level))
        logLevel = level;

      config
        .MinimumLevel.Is(logLevel)
        .Enrich.FromLogContext()
        .WriteTo.Console();

      var elasticUrl = EnvVariable.TryGet(EnvVariables._ElasticUrl);
      if (elasticUrl is not null)
      {
        config.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
        {
          IndexFormat = $"{RoleNames._AuthorityService}-logs-{DateTime.UtcNow:yyyy-MM}",
          AutoRegisterTemplate = true
        });
      }
    }

    public static void SetupRequests(RequestLoggingOptions options)
    {
      options.EnrichDiagnosticContext = (diagContext, httpContext) =>
      {
        var context = httpContext
          .Features
          .Get<LoggingContext>();

        if (context == null)
        {
          context = LoggingContext.Root;

          httpContext
           .Features
           .Set(context);
        }

        diagContext.Set(_ScopeIdPropName, context.ScopeId);
        diagContext.Set(_ParentScopeIdPropName, context.ParentScopeId);
      };

      options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms ScopeId: " +
        "{" + _ScopeIdPropName + "} ParentScopeId: " + "{" + _ParentScopeIdPropName + "}";
    }
  }
}
