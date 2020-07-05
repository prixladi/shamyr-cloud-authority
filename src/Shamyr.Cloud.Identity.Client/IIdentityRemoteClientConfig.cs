using System;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityRemoteClientConfig
  {
    Uri IdentityUrl { get; }
    string ClientId { get; }
    string ClientSecret { get; }
  }
}