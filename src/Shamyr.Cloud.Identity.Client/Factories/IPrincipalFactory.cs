using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  public interface IPrincipalFactory
  {
    Task<ClaimsPrincipal> CreateAsync(IServiceProvider serviceProvider, string challenge, UserIdentityProfileModel model, CancellationToken cancellationToken);
  }
}
