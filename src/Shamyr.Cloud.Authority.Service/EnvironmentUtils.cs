using System;

namespace Shamyr.Cloud.Authority.Service
{
  public static class EnvironmentUtils
  {
    public static Uri PortalUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._PortalUrl));
    public static Uri AuthorityUrl { get; } = new Uri(EnvVariable.Get(EnvVariables._AuthorityUrl));
  }
}
