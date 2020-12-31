using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Authority.Service.Dtos.Clients;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public class ClientRepository: RepositoryBase<ClientDoc>, IClientRepository
  {
    public ClientRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<bool> ExistsByClientNameAsync(string clientName, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.Name == clientName, cancellationToken);
    }

    public async Task<List<ClientDoc>> GetAsync(CancellationToken cancellationToken)
    {
      return await Query.ToListAsync(cancellationToken);
    }

    public async Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken)
    {
      var update = Builders<ClientDoc>.Update
        .Set(doc => doc.Disabled, disabled);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }

    public async Task<bool> UpdateAsync(ObjectId id, UpdateDto updateDto, CancellationToken cancellationToken)
    {
      var update = Builders<ClientDoc>.Update
        .Set(doc => doc.Name, updateDto.Name)
        .Set(doc => doc.RequireEmailVerification, updateDto.RequireEmailVerification)
        .Set(doc => doc.PasswordResetEmailTemplateId, updateDto.PasswordResetEmailTemplateId)
        .Set(doc => doc.VerifyAccountEmailTemplateId, updateDto.VerifyAccountEmailTemplateId)
        .Set(doc => doc.PortalUrl, updateDto.PortalUrl)
        .Set(doc => doc.AuthorityUrl, updateDto.AuthorityUrl)
        .Set(doc => doc.Secret, updateDto.Secret);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }
  }
}
