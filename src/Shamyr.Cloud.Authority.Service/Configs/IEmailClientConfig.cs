using System;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public interface IEmailClientConfig
  {
    public Uri ServerUrl { get; }
    public string SenderAddress { get; }
  }
}