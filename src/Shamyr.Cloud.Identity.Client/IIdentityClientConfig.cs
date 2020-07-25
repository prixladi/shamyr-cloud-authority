using System;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityClientConfig
  {
    Uri IdentityUrl { get; }
    Uri GatewayUrl { get; }
    string ClientId { get; }
    string ClientSecret { get; }
  }
}