using Shamyr.Cloud.Identity.Client.SignalR;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class GatewayHubClientConfig: HubClientConfigBase
  {
    public override string GatewayUrl => EnvVariable.Get(EnvVariables._GatewayUrl);
    public override string ClientId => EnvVariable.Get(EnvVariables._ClientId);
    public override string ClientSecret => EnvVariable.Get(EnvVariables._ClientSecret);
  }
}
