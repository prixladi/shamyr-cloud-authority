using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Client.Factories
{
  public abstract class PrincipalFactoryBase: IPrincipalFactory
  {
    public async Task<ClaimsPrincipal> CreateAsync(IServiceProvider serviceProvider, string authenticationType, UserModel model, CancellationToken cancellationToken)
    {
      var identity = await CreateIdentityAsync(serviceProvider, authenticationType, model, cancellationToken);
      var userPrincipal = new ClaimsPrincipal(identity);

      foreach (var role in await GetRolesAsync(serviceProvider, identity, cancellationToken))
        identity.AddClaim(new Claim(ClaimTypes.Role, role));

      return userPrincipal;
    }

    protected abstract Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserModel model, CancellationToken cancellationToken);
    protected abstract Task<IEnumerable<string>> GetRolesAsync(IServiceProvider serviceProvider, ClaimsIdentity identity, CancellationToken cancellationToken);
  }
}
