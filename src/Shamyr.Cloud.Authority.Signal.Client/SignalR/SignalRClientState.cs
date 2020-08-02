using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  internal class SignalRClientState: IAsyncDisposable
  {
    private readonly IServiceProvider fServiceProvider;
    private readonly ITracker fTracker;

    public IOperationContext Context { get; }
    public HubConnection Connection { get; }
    public Uri SignalUrl { get; }
    public CancellationTokenSource CancellationSource { get; }

    public SignalRClientState(Uri signalUrl, IServiceProvider serviceProvider)
    {
      SignalUrl = signalUrl;
      fServiceProvider = serviceProvider;

      CancellationSource = new CancellationTokenSource();
      Context = OperationContext.Origin;
      fTracker = serviceProvider.GetRequiredService<ITracker>();

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
      using (fTracker.TrackRequest(Context, $"Disconecting from {SignalUrl}.", out var requestContext))
      {
        try
        {
          await Connection.DisposeAsync();
          requestContext.Success();
        }
        catch (Exception ex)
        {
          fTracker.TrackException(requestContext, ex);
          requestContext.Fail();
          throw;
        }
      }
    }
  }
}
