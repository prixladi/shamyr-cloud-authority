using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Client.Factories
{
  public interface IPrincipalFactory
  {
    Task<ClaimsPrincipal> CreateAsync(IServiceProvider serviceProvider, string challenge, UserModel model, CancellationToken cancellationToken);
  }
}
