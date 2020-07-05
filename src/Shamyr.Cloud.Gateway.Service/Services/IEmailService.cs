using Shamyr.Cloud.Emails;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public interface IEmailService
  {
    void SendEmailAsync(EmailBase email);
  }
}
