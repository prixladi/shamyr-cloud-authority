using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Repositories;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Extensions.Hosting;
using Shamyr.Logging;
using Shamyr.Operations;

namespace Shamyr.Cloud.Authority.Client.HostedServices
{
  public class TokenConfigurationCronService: CronServiceBase
  {
    private static readonly TimeSpan fInterval = TimeSpan.FromMinutes(10);

    public TokenConfigurationCronService(IServiceProvider provider)
      : base(fInterval, provider) { }

    protected override async Task ExecuteAsync(IServiceProvider provider, CancellationToken cancellationToken)
    {
      var configurationRepository = provider.GetRequiredService<ITokenConfigurationRepository>();
      var configurationService = provider.GetRequiredService<ITokenConfigurationService>();
      var config = provider.GetRequiredService<IAuthorityClientConfig>();

      Task<TokenConfigurationModel> Operation(ILoggingContext context, CancellationToken cancellationToken)
      {
        return configurationService.GetAsync(config.AuthorityUrl, context, cancellationToken);
      }

      var operationConfig = new RetryOperationConfig
      {
        RetryCount = configurationRepository.IsSet() ? 3 : (int?)null,
        RetryPolicy = new RetryPolicy(TimeSpan.FromSeconds(30), 2),
        SameExceptions = SameExceptions
      };

      using (fLogger.TrackDependency(fContext, "REST", "Token configuration service loading.", RoleNames._AuthorityService, string.Empty, out var trackingContext))
      {
        await new RetryOperation<TokenConfigurationModel>(Operation, operationConfig)
         .Catch<TokenConfigurationModel, HttpRequestException>(fLogger)
         .Catch<TokenConfigurationModel, Exception>(fLogger, true)
         .OnFail(trackingContext)
         .OnFail(fLogger, $"Unable to load authority configuration. Signing key may be deprecated!")
         .OnSuccess(trackingContext)
         .OnSuccess(fLogger, $"Loaded new authority configuration.")
         .OnSuccess((result, contex) => configurationRepository.Set(result))
         .ExecuteAsync(trackingContext, cancellationToken);
      }
    }

    private bool SameExceptions(Exception ex1, Exception ex2)
    {
      if (ex1 is HttpRequestException he1 && ex2 is HttpRequestException he2)
        return he1.StatusCode == he2.StatusCode;

      return ex1.GetType() == ex2.GetType();
    }
  }
}
