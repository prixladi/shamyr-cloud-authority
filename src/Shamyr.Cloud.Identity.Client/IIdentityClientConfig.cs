using System;
using Shamyr.Cloud.Authority.Client;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityClientConfig: IAuthorityClientConfig
  {
    Uri IdentityUrl { get; }
    string ClientId { get; }
    string ClientSecret { get; }
  }
}