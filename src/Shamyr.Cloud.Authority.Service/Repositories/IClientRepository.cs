using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Dtos.Clients;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public interface IClientRepository: IRepositoryBase<ClientDoc>
  {
    Task<bool> ExistsByClientNameAsync(string clientName, CancellationToken cancellationToken);
    Task<List<ClientDoc>> GetAsync(CancellationToken cancellationToken);
    Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, ClientUpdateDto updateDto, CancellationToken cancellationToken);
  }
}