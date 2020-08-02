using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public interface ITokenConfigurationService
  {
    Task<TokenConfigurationModel> GetAsync(Uri authorityUrl, IOperationContext context, CancellationToken cancellationToken);
  }
}