using System;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public class EmailClientConfig: IEmailClientConfig
  {
    public Uri ServerUrl => EnvironmentUtils.EmailServerUrl;
    public string SenderAddress => EnvironmentUtils.EmailSenderAddress;
  }
}
