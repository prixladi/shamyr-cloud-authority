using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public interface ITokenConfigurationService
  {
    Task<TokenConfigurationModel> GetAsync(Uri authorityUrl, ILoggingContext context, CancellationToken cancellationToken);
  }
}