using System;
using Shamyr.Cloud.Authority.Client.SignalR;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Identity.Client.SignalR
{
  public class SignalRClientConfig: IAuthoritySignalRClientConfig
  {
    private readonly IIdentityClientConfig fConfig;

    public SignalRClientConfig(IIdentityClientConfig config)
    {
      fConfig = config;
    }

    public Uri AuthorityUrl => fConfig.AuthorityUrl;
    public string ClientId => fConfig.ClientId;
    public string ClientSecret => fConfig.ClientSecret;

    public string[] SubscribedResources => new string[]
    {
      Resources._UserAdminChanged,
      Resources._UserDisabledChanged,
      Resources._UserVerifiedChanged,
      Resources._UserLoggedOut
    };
  }
}