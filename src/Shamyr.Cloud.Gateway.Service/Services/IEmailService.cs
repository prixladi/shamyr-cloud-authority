using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Service.Emails;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public interface IEmailService
  {
    Task SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken);
  }
}
