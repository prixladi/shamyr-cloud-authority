using System.Net.Mail;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class EmailDto
  {
    public MailAddress RecipientAddress { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool IsBodyHtml { get; set; }
  }
}
