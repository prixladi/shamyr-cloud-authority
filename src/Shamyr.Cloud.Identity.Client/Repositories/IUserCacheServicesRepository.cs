using System;
using System.Collections.Generic;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  public interface IUserCacheServicesRepository
  {
    IEnumerable<IUserCacheService> GetCacheServices(IServiceProvider serviceProvider);
  }
}