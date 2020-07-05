using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Identity.Service.Repositories
{
  public interface IClientRepository
  {
    Task<ClientDoc?> GetAsync(ObjectId id, CancellationToken cancellationToken);
  }
}