using System;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public class EmailClientConfing: IEmailClientConfig
  {
    public Uri ServerUrl => EnvironmentUtils.EmailServerUrl;
    public string SenderAddress => EnvironmentUtils.EmailSenderAddress;
  }
}
