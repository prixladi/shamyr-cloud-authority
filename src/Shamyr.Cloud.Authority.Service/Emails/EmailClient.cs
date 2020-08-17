using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Configs;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public class EmailClient: IEmailClient
  {
    private readonly IEmailClientConfig fConfig;
    private readonly ILogger fLogger;

    public EmailClient(IEmailClientConfig config, ILogger logger)
    {
      fConfig = config;
      fLogger = logger;
    }

    public async Task SendEmailAsync(MailAddress recipient, string subject, EmailBody body, ILoggingContext context)
    {
      if (recipient is null)
        throw new ArgumentNullException(nameof(recipient));
      if (subject is null)
        throw new ArgumentNullException(nameof(subject));
      if (body is null)
        throw new ArgumentNullException(nameof(body));

      using (fLogger.TrackRequest(context, $"Sending email to {recipient.Address}.", out var requestContext))
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
          fLogger.LogInformation(requestContext, $"Email with subject '{subject}' from '{fConfig.SenderAddress}' to '{recipient.Address}' succesfuly sent.");
        }
        catch (Exception ex)
        {
          requestContext.Fail();
          fLogger.LogException(requestContext, ex, $"Error occured while sending with subject '{subject}' from '{fConfig.SenderAddress}' to '{recipient.Address}'.");
        }
      }
    }
  }
}
