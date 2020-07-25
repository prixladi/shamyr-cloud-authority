using System;
using System.Collections.Generic;
using Shamyr.Cloud.Identity.Client.Services;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  public interface IUserCacheServicesRepository
  {
    IEnumerable<IUserCacheService> GetCacheServices(IServiceProvider serviceProvider);
  }
}