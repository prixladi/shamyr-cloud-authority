using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class PrincipalFactory: PrincipalFactoryBase
  {
    protected override Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserIdentityProfileModel model, CancellationToken cancellationToken)
    {
      return Task.FromResult(new ClaimsIdentity(new Claim[] { new Claim("id", model.Id.ToString()) }, authenticationType));
    }

    protected override IAsyncEnumerable<string> GetRolesAsync(IServiceProvider serviceProvider, ClaimsIdentity identity, CancellationToken cancellationToken)
    {
      return AsyncEnumerable.Empty<string>();
    }
  }
}
