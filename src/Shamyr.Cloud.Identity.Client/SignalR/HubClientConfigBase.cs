using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.SignalR
{
  public abstract class HubClientConfigBase: IHubClientConfig
  {
    public abstract string GatewayUrl { get; }

    public abstract string ClientId { get; }

    public abstract string ClientSecret { get; }

    public string[] SubscribedResources => new string[]
    {
      Resources._UserLoggedOut,
      Resources._UserUserPermissionChanged,
      Resources._UserVerificationStatusChanged
    };
  }
}