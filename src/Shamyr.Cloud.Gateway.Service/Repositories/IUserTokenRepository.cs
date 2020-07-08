using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories
{
  public interface IUserTokenRepository: INestedRepositoryBase<UserDoc, TokenDoc>
  {
    Task<TokenDoc> GetAsync(ObjectId userId, CancellationToken cancellationToken);
    Task SetRefreshTokenAsync(ObjectId userId, TokenDoc userToken, CancellationToken cancellationToken);
  }
}