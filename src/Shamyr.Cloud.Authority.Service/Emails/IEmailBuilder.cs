using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public interface IEmailBuilder
  {
    bool CanBuild(IEmailBuildContext context);
    Task<EmailDto?> TryBuildAsync(IEmailBuildContext context, CancellationToken cancellationToken);
  }
}
