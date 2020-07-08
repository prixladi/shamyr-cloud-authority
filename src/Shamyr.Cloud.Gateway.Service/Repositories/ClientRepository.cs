using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos.Clients;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories
{
  public class ClientRepository: RepositoryBase<ClientDoc>, IClientRepository
  {
    public ClientRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<bool> ExistsByClientNameAsync(string clientName, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.ClientName == clientName, cancellationToken);
    }

    public async Task<List<ClientDoc>> GetAsync(CancellationToken cancellationToken)
    {
      return await Query.ToListAsync(cancellationToken);
    }

    public async Task<ClientDoc?> GetByClientNameAsync(string clientName, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.ClientName == clientName, cancellationToken);
    }

    public async Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken)
    {
      var update = Builders<ClientDoc>.Update
        .Set(doc => doc.Disabled, disabled);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }

    public async Task<bool> UpdateAsync(ObjectId id, ClientUpdateDto updateDto, CancellationToken cancellationToken)
    {
      var update = Builders<ClientDoc>.Update
        .Set(x => x.ClientName, updateDto.ClientName)
        .Set(x => x.Secret, updateDto.ClientSecret);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }
  }
}
