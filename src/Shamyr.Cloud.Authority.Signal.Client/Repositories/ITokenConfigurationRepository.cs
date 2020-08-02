using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;

namespace Shamyr.Cloud.Authority.Client.Repositories
{
  public interface ITokenConfigurationRepository
  {
    Task<TokenConfigurationModel> GetAsync(CancellationToken cancellationToken);
    bool IsSet();
    void Set(TokenConfigurationModel configuration);
  }
}