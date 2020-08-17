using System;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public class EmailClientConfig: IEmailClientConfig
  {
    public Uri ServerUrl => new Uri(EnvVariable.Get(EnvVariables._EmailServerUrl));
    public string SenderAddress => EnvVariable.Get(EnvVariables._EmailSenderAddress);
  }
}
