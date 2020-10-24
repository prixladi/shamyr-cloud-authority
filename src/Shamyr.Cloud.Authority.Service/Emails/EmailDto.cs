using System.Net.Mail;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public record EmailDto(
    MailAddress RecipientAddress,
    string Subject,
    string Body,
    bool IsBodyHtml);
}
