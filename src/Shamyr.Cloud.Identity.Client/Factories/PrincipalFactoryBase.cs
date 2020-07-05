using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  public abstract class PrincipalFactoryBase: IPrincipalFactory
  {
    public async Task<ClaimsPrincipal> CreateAsync(IServiceProvider serviceProvider, string authenticationType, UserIdentityProfileModel model, CancellationToken cancellationToken)
    {
      var identity = await CreateIdentityAsync(serviceProvider, authenticationType, model, cancellationToken);

      var userPrincipal = new ClaimsPrincipal(identity);

      var roles = GetRolesAsync(serviceProvider, identity, cancellationToken);
      await foreach (var role in roles)
        identity.AddClaim(new Claim(ClaimTypes.Role, role));

      return userPrincipal;
    }

    protected abstract Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserIdentityProfileModel model, CancellationToken cancellationToken);
    protected abstract IAsyncEnumerable<string> GetRolesAsync(IServiceProvider serviceProvider, ClaimsIdentity identity, CancellationToken cancellationToken);
  }
}
