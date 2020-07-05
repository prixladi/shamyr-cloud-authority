using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal class RetryPolicy: IRetryPolicy
  {
    private readonly double fMaxTimeout;
    private readonly int fCoefficient;

    public RetryPolicy(TimeSpan maxTimeout, int coefficient)
    {
      fMaxTimeout = maxTimeout.TotalSeconds;
      fCoefficient = coefficient;
    }

    public TimeSpan? NextRetryDelay(RetryContext retryContext)
    {
      var delay = Math.Min(fMaxTimeout, Math.Pow(fCoefficient, retryContext.PreviousRetryCount));
      return TimeSpan.FromSeconds(delay);
    }
  }
}
