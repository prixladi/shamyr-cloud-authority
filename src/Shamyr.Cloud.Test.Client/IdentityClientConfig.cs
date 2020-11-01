using System;
using Shamyr.Cloud.Identity.Client;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class IdentityClientConfig: IIdentityClientConfig
  {
    public Uri IdentityUrl => new Uri(EnvVariable.Get(EnvVariables._IdentityUrl));
    public Uri AuthorityUrl => new Uri(EnvVariable.Get(EnvVariables._AuthorityUrl));
    public string ClientId => EnvVariable.Get(EnvVariables._ClientId);
    public string ClientSecret => EnvVariable.Get(EnvVariables._ClientSecret);
  }
}
