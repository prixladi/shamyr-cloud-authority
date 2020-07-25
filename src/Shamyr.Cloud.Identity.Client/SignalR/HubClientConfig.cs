using System;
using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.SignalR
{
  public class HubClientConfig: IHubClientConfig
  {
    private readonly IIdentityClientConfig fConfig;

    public HubClientConfig(IIdentityClientConfig config)
    {
      fConfig = config;
    }

    public Uri GatewayUrl => fConfig.IdentityUrl;
    public string ClientId => fConfig.ClientId;
    public string ClientSecret => fConfig.ClientSecret;

    public string[] SubscribedResources => new string[]
    {
      Resources._UserLoggedOut,
      Resources._UserUserPermissionChanged,
      Resources._UserVerificationStatusChanged
    };
  }
}