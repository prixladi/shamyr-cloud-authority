using System;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class IdentityRemoteClientConfig: IIdentityRemoteClientConfig
  {
    public Uri IdentityUrl => new Uri(EnvVariable.Get(EnvVariables._IdentityUrl));
    public string ClientId => EnvVariable.Get(EnvVariables._ClientId);
    public string ClientSecret => EnvVariable.Get(EnvVariables._ClientSecret);
  }
}
