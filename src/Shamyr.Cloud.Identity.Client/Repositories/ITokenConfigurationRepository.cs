using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  public interface ITokenConfigurationRepository
  {
    Task<TokenConfigurationModel> GetAsync(CancellationToken cancellationToken);
    void Set(TokenConfigurationModel configuration);
  }
}