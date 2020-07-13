using System;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public interface IEmailClientConfig
  {
    public Uri ServerUrl { get;  }
    public string SenderAddress { get; }
  }
}