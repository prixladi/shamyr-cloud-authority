using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Cloud.Authority.Service.Factories;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public class EmailService: IEmailService
  {
    private readonly ITracker fTracker;
    private readonly IEmailClient fEmailClient;
    private readonly IEmailBuilderFactory fEmailBuilderFactory;

    public EmailService(IEmailClient emailClient, IEmailBuilderFactory emailBuilderFactory, ITracker tracker)
    {
      fEmailClient = emailClient;
      fEmailBuilderFactory = emailBuilderFactory;
      fTracker = tracker;
    }

    public async Task SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken)
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

      await fEmailClient.SendEmailAsync(dto.RecipientAddress, dto.Subject, new EmailBody(dto.Body, dto.IsBodyHtml), context);
    }
  }
}
