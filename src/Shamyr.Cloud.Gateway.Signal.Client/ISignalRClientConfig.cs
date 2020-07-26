using System;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  public interface ISignalRClientConfig
  {
    Uri GatewayUrl { get; }
    string ClientId { get; }
    string ClientSecret { get; }

    public string[] SubscribedResources { get; }
  }
}
