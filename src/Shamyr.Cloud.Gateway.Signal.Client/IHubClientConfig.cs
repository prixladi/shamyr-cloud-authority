using System;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  public interface IHubClientConfig
  {
    Uri GatewayUrl { get; }
    string ClientId { get; }
    string ClientSecret { get; }

    public string[] SubscribedResources { get; }
  }
}
