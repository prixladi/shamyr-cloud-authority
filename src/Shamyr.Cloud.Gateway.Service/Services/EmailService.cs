using System.Threading;
using Shamyr.Cloud.Gateway.Service.Emails;
using Shamyr.Cloud.Gateway.Service.Factories;
using Shamyr.Emails;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public class EmailService: IEmailService
  {
    private readonly ITracker fTracker;
    private readonly IEmailClient fEmailClient;
    private readonly IEmailBuilderFactory fEmailBuilderFactory;

    public EmailService(
      ITracker tracker,
      IEmailClient emailClient,
      IEmailBuilderFactory emailBuilderFactory)
    {
      fTracker = tracker;
      fEmailClient = emailClient;
      fEmailBuilderFactory = emailBuilderFactory;
    }

    public async void SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken)
    {
      var builder = fEmailBuilderFactory.TryCreate(context);
      if (builder == null)
      {
        fTracker.TrackError(context, $"For email build context of type '{context.GetType()}' does not exist builder.");
        return;
      }

      var dto = await builder.TryBuildAsync(context, cancellationToken);
      if (dto == null)
      {
        fTracker.TrackError(context, $"For email '{context.EmailType}' does not exist template in DB.");
        return;
      }

      await fEmailClient.SendEmailAsync(context, dto.RecipientAddress, dto.Subject, new EmailBody(dto.Body, true));
    }
  }
}
