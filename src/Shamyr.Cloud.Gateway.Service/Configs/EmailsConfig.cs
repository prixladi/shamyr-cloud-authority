using Shamyr.Emails.Smtp;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public class EmailsConfig: ISmtpEmailConfig
  {
    public string Host => EnvironmentUtils.EmailHost;
    public int Port => EnvironmentUtils.EmailPort;
    public bool UseSsl => EnvironmentUtils.EmailUseSsl;
    public string SubjectSuffix => EnvironmentUtils.EmailSubjectSuffix;
    public string SenderAlias => EnvironmentUtils.EmailSenderAlias;
    public string SenderAddress => EnvironmentUtils.EmailSenderAddress;
    public string SenderAdresssPassword => EnvironmentUtils.EmailSenderAddressPassword;
  }
}
