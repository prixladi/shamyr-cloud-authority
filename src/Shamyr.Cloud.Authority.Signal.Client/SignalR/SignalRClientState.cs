using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  internal class SignalRClientState: IAsyncDisposable
  {
    private readonly IServiceProvider fServiceProvider;
    private readonly ILogger fLogger;

    public ILoggingContext Context { get; }
    public HubConnection Connection { get; }
    public Uri SignalUrl { get; }
    public CancellationTokenSource CancellationSource { get; }

    public SignalRClientState(Uri signalUrl, IServiceProvider serviceProvider)
    {
      SignalUrl = signalUrl;
      fServiceProvider = serviceProvider;

      CancellationSource = new CancellationTokenSource();
      Context = LoggingContext.Root;
      fLogger = serviceProvider.GetRequiredService<ILogger>();

      using var scope = fServiceProvider.CreateScope();
      var hubService = scope.ServiceProvider.GetRequiredService<IHubConnectionService>();
      Connection = hubService.CreateConnection(signalUrl);
    }

    public async Task InitialConnectAsync(CancellationToken cancellationToken)
    {
      using var scope = fServiceProvider.CreateScope();
      var hubService = scope.ServiceProvider.GetRequiredService<IHubConnectionService>();
      await hubService.InitialConnectAsync(Connection, SignalUrl, Context, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
      CancellationSource.Cancel();
      using (fLogger.TrackRequest(Context, $"Disconecting from {SignalUrl}.", out var requestContext))
      {
        try
        {
          await Connection.DisposeAsync();
          requestContext.Success();
        }
        catch (Exception ex)
        {
          fLogger.LogException(requestContext, ex);
          requestContext.Fail();
          throw;
        }
      }
    }
  }
}
