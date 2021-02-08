using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Client.Test
{
  public class PrincipalFactory: PrincipalFactoryBase
  {
    protected override Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserModel model, CancellationToken cancellationToken)
    {
      return Task.FromResult(new ClaimsIdentity(new Claim[] { new Claim("id", model.Id) }, authenticationType));
    }
  }
}
