using Shamyr.Threading;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  public interface IUserLockRepository
  {
    AsyncReaderWriterLock GetByKey(string key);
  }
}