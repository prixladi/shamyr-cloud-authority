using System;
using System.Collections.Generic;
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
        yield return (IUserCacheService)serviceProvider.GetService(type)!;
    }
  }
}
