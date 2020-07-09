using System.Threading;
using Shamyr.Cloud.Emails;
using Shamyr.Cloud.Gateway.Service.Emails;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public interface IEmailService
  {
    void SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken);
  }
}
