using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Users
{
  public class UserTokenRepository: NestedRepositoryBase<UserDoc, TokenDoc>, IUserTokenRepository
  {
    protected override Expression<Func<UserDoc, TokenDoc>> Map => doc => doc.RefreshToken!;

    public UserTokenRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<TokenDoc> GetAsync(ObjectId userId, CancellationToken cancellationToken)
    {
      return await GetByDocumentIdAsync(userId, cancellationToken);
    }

    public async Task SetRefreshTokenAsync(ObjectId userId, TokenDoc userToken, CancellationToken cancellationToken)
    {
      await SetByDocumentIdAsync(userId, userToken, cancellationToken);
    }
  }
}
