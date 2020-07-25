using System;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class IdentityClientConfig: IIdentityClientConfig
  {
    public Uri IdentityUrl => new Uri(EnvVariable.Get(EnvVariables._IdentityUrl));
    public Uri GatewayUrl => new Uri(EnvVariable.Get(EnvVariables._GatewayUrl));
    public string ClientId => EnvVariable.Get(EnvVariables._ClientId);
    public string ClientSecret => EnvVariable.Get(EnvVariables._ClientSecret);
  }
}
