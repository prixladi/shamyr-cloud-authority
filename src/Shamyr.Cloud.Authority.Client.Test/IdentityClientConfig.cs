using System;

namespace Shamyr.Cloud.Authority.Client.Test
{
  public class IdentityClientConfig: IAuthorityClientConfig
  {
    public Uri AuthorityUrl => new Uri(EnvVariable.Get(EnvVariables._AuthorityUrl));
  }
}
