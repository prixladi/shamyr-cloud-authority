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
      var elasticUrl = EnvVariable.Get(EnvVariables._ElasticUrl);
      LogEventLevel logLevel = LogEventLevel.Warning;
      if(Enum.TryParse(EnvVariable.TryGet(EnvVariables._LogLevel), true, out LogEventLevel level))
        logLevel = level;

      config
        .MinimumLevel.Is(logLevel)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
        {
          IndexFormat = $"{RoleNames._AuthorityService}-logs-{DateTime.UtcNow:yyyy-MM}"
        });
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
