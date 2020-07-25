using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Token.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface ITokenConfigurationService
  {
    Task<TokenConfigurationModel?> TryGetAsync(Uri gatewayUrl, IOperationContext context, CancellationToken cancellationToken);
  }
}