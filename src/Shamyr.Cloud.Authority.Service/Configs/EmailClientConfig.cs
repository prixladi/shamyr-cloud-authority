using System;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class EmailClientConfig: IEmailClientConfig
  {
    public Uri ServerUrl => EnvironmentUtils.EmailServerUrl;
    public string SenderAddress => EnvironmentUtils.EmailSenderAddress;
  }
}
