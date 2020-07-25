using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  [Singleton]
  internal class UserCacheServicesRepository: IUserCacheServicesRepository
  {
    public LinkedList<Type> fUserCacheServiceTypes;

    public UserCacheServicesRepository()
    {
      fUserCacheServiceTypes = new LinkedList<Type>();
    }

    public void AddService<T>()
      where T : IUserCacheService
    {
      fUserCacheServiceTypes.AddLast(typeof(T));
    }

    public IEnumerable<IUserCacheService> GetCacheServices(IServiceProvider serviceProvider)
    {
      foreach (var type in fUserCacheServiceTypes)
        yield return (IUserCacheService)serviceProvider.GetRequiredService(type);
    }
  }
}
