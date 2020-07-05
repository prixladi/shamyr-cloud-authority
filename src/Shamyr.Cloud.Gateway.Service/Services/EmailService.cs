using System;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Emails;
using Shamyr.Emails;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public class EmailService: IEmailService
  {
    private readonly IEmailClient fEmailClient;
    private readonly ITelemetryService fTelemetryService;

    public EmailService(IEmailClient emailClient, ITelemetryService telemetryService)
    {
      fEmailClient = emailClient;
      fTelemetryService = telemetryService;
    }

    public async void SendEmailAsync(EmailBase email)
    {
      if (email is null)
        throw new ArgumentNullException(nameof(email));

      var context = fTelemetryService.GetRequestContext();
      await fEmailClient.SendEmailAsync(context, email.RecipientAddress, email.Subject, new EmailBody(email.Body, true));
    }
  }
}
