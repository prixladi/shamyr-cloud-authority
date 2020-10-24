using System;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  internal partial class SignalRClient
  {
    private Task ClosedAsync(Exception ex)
    {
      if (ex is null)
        fLogger.LogError(fState.Context, $"Connection with url '{fState.SignalUrl}' closed.");
      else
        fLogger.LogException(fState.Context, ex, $"Connection with url '{fState.SignalUrl}' closed with exception.");

      return Task.CompletedTask;
    }

    private Task ReconnectingAsync(Exception ex)
    {
      if (ex is null)
        fLogger.LogInformation(fState.Context, $"Reconecting to url '{fState.SignalUrl}'.");
      else
        fLogger.LogException(fState.Context, ex, $"Reconecting to url '{fState.SignalUrl}'.");

      return Task.CompletedTask;
    }

    private Task ReconnectedAsync(string connectionId)
    {
      if (string.IsNullOrEmpty(connectionId))
        fLogger.LogInformation(fState.Context, $"Reconected to url '{fState.SignalUrl}' with old connection id.");
      else
        fLogger.LogInformation(fState.Context, $"Reconected to url '{fState.SignalUrl}' with connection id '{connectionId}'.");

      if (fAuthroizeTask is null || fAuthroizeTask.IsCompleted || fAuthroizeTask.IsFaulted || fAuthroizeTask.IsCanceled)
        fAuthroizeTask = AuthorizeConnectionAsync(fState.Context, fState.CancellationSource.Token);

      return Task.CompletedTask;
    }
  }
}
