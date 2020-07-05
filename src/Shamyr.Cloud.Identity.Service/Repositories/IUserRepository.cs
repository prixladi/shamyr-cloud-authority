using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Identity.Service.Repositories
{
  public interface IUserRepository
  {
    Task<UserDoc?> GetAsync(ObjectId id, CancellationToken cancellationToken);
  }
}
