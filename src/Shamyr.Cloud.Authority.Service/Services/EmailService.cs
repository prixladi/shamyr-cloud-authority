using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Cloud.Authority.Service.Factories;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public class EmailService: IEmailService
  {
    private readonly ILogger fLogger;
    private readonly IEmailClient fEmailClient;
    private readonly IEmailBuilderFactory fEmailBuilderFactory;

    public EmailService(IEmailClient emailClient, IEmailBuilderFactory emailBuilderFactory, ILogger logger)
    {
      fEmailClient = emailClient;
      fEmailBuilderFactory = emailBuilderFactory;
      fLogger = logger;
    }

    public async Task SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken)
    {
      var builder = fEmailBuilderFactory.TryCreate(context);
      if (builder is null)
      {
        fLogger.LogError(context, $"For email build context of type '{context.GetType()}' doesn't exist any builder.");
        return;
      }

      var dto = await builder.TryBuildAsync(context, cancellationToken);
      if (dto is null)
      {
        fLogger.LogError(context, $"For email '{context.EmailType}' does not exist template in DB.");
        return;
      }

      await fEmailClient.SendEmailAsync(dto.RecipientAddress, dto.Subject, new EmailBody(dto.Body, dto.IsBodyHtml), context);
    }
  }
}
