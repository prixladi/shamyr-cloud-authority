using System.Collections.Concurrent;
using Shamyr.DependencyInjection;
using Shamyr.Threading;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  [Singleton]
  internal class UserLockRepository: IUserLockRepository
  {
    private readonly ConcurrentDictionary<string, AsyncReaderWriterLock> fLocks;

    public UserLockRepository()
    {
      fLocks = new ConcurrentDictionary<string, AsyncReaderWriterLock>();
    }

    public AsyncReaderWriterLock GetByKey(string key)
    {
      return fLocks.GetOrAdd(key, _ => new AsyncReaderWriterLock());
    }
  }
}
