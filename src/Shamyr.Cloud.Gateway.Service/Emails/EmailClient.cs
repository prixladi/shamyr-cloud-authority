using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Service.Configs;
using Shamyr.Emails;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class EmailClient: IEmailClient
  {
    private readonly IEmailClientConfig fConfig;
    private readonly ITracker fTracker;

    public EmailClient(IEmailClientConfig config,ITracker tracker)
    {
      fConfig = config;
      fTracker = tracker;
    }

    public async Task SendEmailAsync(IOperationContext context, MailAddress recipient, string subject, EmailBody body, bool trowIfError = false)
    {
      if (recipient is null)
        throw new ArgumentNullException(nameof(recipient));
      if (subject is null)
        throw new ArgumentNullException(nameof(subject));
      if (body is null)
        throw new ArgumentNullException(nameof(body));

      using (fTracker.TrackRequest(context, $"Sending email to {recipient.Address}.", out var requestContext))
      {
        try
        {
          var formContent = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string?, string?>("to", recipient.Address),
              new KeyValuePair<string?, string?>("subject", subject),
              new KeyValuePair<string?, string?>("message", body.Content),
              new KeyValuePair<string?, string?>("isBodyHtml", body.IsHtml.ToString()),
              new KeyValuePair<string?, string?>("sender", fConfig.SenderAddress)
          });

          await HttpClientContext.Client.PostAsync(fConfig.ServerUrl, formContent);
          requestContext.Success();
          fTracker.TrackInformation(requestContext, $"Email with subject '{subject}' from '{fConfig.SenderAddress}' to '{recipient.Address}' succesfuly sent.");
        }
        catch(Exception ex)
        {
          requestContext.Fail();
          fTracker.TrackException(requestContext, ex, $"Error occured while sending with subject '{subject}' from '{fConfig.SenderAddress}' to '{recipient.Address}'.");
        }
      }
    }
  }
}
