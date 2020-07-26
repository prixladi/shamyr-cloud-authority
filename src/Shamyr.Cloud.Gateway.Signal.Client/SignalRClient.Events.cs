using System;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal partial class SignalRClient
  {
    private Task ClosedAsync(Exception ex)
    {
      if (ex is null)
        fTracker.TrackError(fState.Context, $"Connection with url '{fState.SignalUrl}' closed.");
      else
        fTracker.TrackException(fState.Context, ex, $"Connection with url '{fState.SignalUrl}' closed with exception.");

      return Task.CompletedTask;
    }

    private Task ReconnectingAsync(Exception ex)
    {
      if (ex is null)
        fTracker.TrackInformation(fState.Context, $"Reconecting to url '{fState.SignalUrl}'.");
      else
        fTracker.TrackException(fState.Context, ex, $"Reconecting to url '{fState.SignalUrl}'.");

      return Task.CompletedTask;
    }

    private async Task ReconnectedAsync(string connectionId)
    {
      if (string.IsNullOrEmpty(connectionId))
        fTracker.TrackInformation(fState.Context, $"Reconected to url '{fState.SignalUrl}' with old connection id.");
      else
        fTracker.TrackInformation(fState.Context, $"Reconected to url '{fState.SignalUrl}' with connection id '{connectionId}'.");

      if (fAuthroizeTask != null)
        await fAuthroizeTask;
      fAuthroizeTask = AuthorizeConnectionAsync(fState.Context, fState.CancellationSource.Token);
    }
  }
}
