using System;
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
      return userPrincipal;
    }

    protected abstract Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserModel model, CancellationToken cancellationToken);
  }
}
