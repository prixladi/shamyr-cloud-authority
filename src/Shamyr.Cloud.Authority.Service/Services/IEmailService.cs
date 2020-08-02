using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Emails;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public interface IEmailService
  {
    Task SendEmailAsync(IEmailBuildContext context, CancellationToken cancellationToken);
  }
}
