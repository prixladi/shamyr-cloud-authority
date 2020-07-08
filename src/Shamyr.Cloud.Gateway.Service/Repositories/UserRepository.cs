using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;
using Shamyr.MongoDB.Sorting;

namespace Shamyr.Cloud.Gateway.Service.Repositories
{
  public class UserRepository: RepositoryBase<UserDoc>, IUserRepository
  {
    public UserRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<List<UserDoc>> GetSortedUsersAsync(UserQueryFilter filter, OrderDefinition<UserDoc>? sort, CancellationToken cancellationToken)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      return await Query
        .WhereUsernameContains(filter.Username)
        .WhereEmailContains(filter.Email)
        .WhereAdmin(filter.Admin)
        .Sort(sort)
        .Pagination(filter)
        .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUserCountAsync(UserQueryFilter filter, CancellationToken cancellationToken)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      return await Query
        .WhereUsernameContains(filter.Username)
        .WhereEmailContains(filter.Email)
        .WhereAdmin(filter.Admin)
        .CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.NormalizedEmail == normalizedEmail, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string normalizedUsername, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.NormalizedUsername == normalizedUsername, cancellationToken);
    }

    public async Task<UserDoc?> GetByUsernameAsync(string normalizedUsername, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.NormalizedUsername == normalizedUsername, cancellationToken);
    }

    public async Task<UserDoc?> GetByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.NormalizedEmail == normalizedEmail, cancellationToken);
    }

    public Task LogoutAsync(ObjectId id, DateTime logoutUtc, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.LogoutUtc, logoutUtc)
        .Unset(doc => doc.RefreshToken);

      return UpdateAsync(id, update, cancellationToken);
    }

    public async Task<bool> TryUnsetEmailTokenAsync(ObjectId id, string emailToken, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
       .Set(doc => doc.EmailToken, null);

      var result = await Collection.UpdateOneAsync(
        doc => doc.Id == id && doc.EmailToken == emailToken,
        update, null, cancellationToken);

      return result.MatchedCount == 1;
    }

    public async Task<UserDoc?> SetPasswordTokenAsync(string email, string passwordToken, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.PasswordToken, passwordToken);

      return await Collection.FindOneAndUpdateAsync<UserDoc>(
        doc => doc.Email == email, update,
        new FindOneAndUpdateOptions<UserDoc, UserDoc> { ReturnDocument = ReturnDocument.After }, cancellationToken);
    }

    public async Task SetUserSecretAsync(ObjectId id, SecretDoc secret, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Secret, secret)
        .Set(doc => doc.PasswordToken, null);

      await UpdateAsync(id, update, cancellationToken);
    }

    public async Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Disabled, disabled);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }
  }
}
