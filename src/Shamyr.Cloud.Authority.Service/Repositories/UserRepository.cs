using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Authority.Service.Dtos.Users;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;
using Shamyr.MongoDB.Sorting;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public class UserRepository: RepositoryBase<UserDoc>, IUserRepository
  {
    public UserRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<List<UserDoc>> GetAsync(FilterDto filter, OrderDefinition<UserDoc>? sort, CancellationToken cancellationToken)
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

    public async Task<int> GetUserCountAsync(FilterDto filter, CancellationToken cancellationToken)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      return await Query
        .WhereUsernameContains(filter.Username)
        .WhereEmailContains(filter.Email)
        .WhereAdmin(filter.Admin)
        .CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.NormalizedEmail == email.CompareNormalize(), cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.NormalizedUsername == username.CompareNormalize(), cancellationToken);
    }

    public async Task<UserDoc?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.NormalizedEmail == email.CompareNormalize(), cancellationToken);
    }

    public async Task<UserDoc?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.NormalizedUsername == username.CompareNormalize(), cancellationToken);
    }

    public async Task<UserDoc?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
      return await Query
        .Where(doc => doc.RefreshToken != null && doc.RefreshToken.Value == refreshToken)
        .SingleOrDefaultAsync(cancellationToken);
    }

    public Task LogoutAsync(ObjectId id, CancellationToken cancellationToken)
    {
      var scewedTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(-1));

      var update = Builders<UserDoc>.Update
        .Set(doc => doc.LogoutUtc, scewedTime)
        .Unset(doc => doc.RefreshToken);

      return UpdateAsync(id, update, cancellationToken);
    }

    public async Task<bool> TryUnsetEmailTokenAndSetVerifiedAsync(ObjectId id, string emailToken, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
       .Unset(doc => doc.EmailToken)
       .Set(doc => doc.Verified, true);

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
        doc => doc.NormalizedEmail == email.CompareNormalize(), update,
        new FindOneAndUpdateOptions<UserDoc, UserDoc> { ReturnDocument = ReturnDocument.After }, cancellationToken);
    }

    public async Task SetSecretAsync(ObjectId id, SecretDoc secret, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Secret, secret)
        .Set(doc => doc.PasswordToken, null);

      await UpdateAsync(id, update, cancellationToken);
    }

    public async Task<bool> TryAddSecretAsync(ObjectId id, SecretDoc secret, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Secret, secret)
        .Set(doc => doc.PasswordToken, null);

      var result = await Collection.UpdateOneAsync(
        doc => doc.Id == id && doc.Secret == null,
        update, cancellationToken: cancellationToken);

      return result.MatchedCount == 1;
    }

    public async Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Disabled, disabled);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }

    public async Task<bool> TrySetAdminAsync(ObjectId id, bool admin, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
        .Set(doc => doc.Admin, admin);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }

    public async Task<UserDoc?> GetByEmailAndVerifyAsync(string email, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
       .Unset(doc => doc.EmailToken)
       .Set(doc => doc.Verified, true);

      return await Collection.FindOneAndUpdateAsync<UserDoc>(
        doc => doc.NormalizedEmail == email.CompareNormalize(),
        update, new FindOneAndUpdateOptions<UserDoc, UserDoc> { ReturnDocument = ReturnDocument.After }, cancellationToken);
    }

    public async Task UpdateAsync(ObjectId userId, UpdateDto updateDto, CancellationToken cancellationToken)
    {
      var update = Builders<UserDoc>.Update
       .Set(doc => doc.Username, updateDto.Username)
       .Set(doc => doc.NormalizedUsername, updateDto.Username.CompareNormalize())
       .Set(doc => doc.GivenName, updateDto.GivenName)
       .Set(doc => doc.FamilyName, updateDto.FamilyName);

      await UpdateAsync(userId, update, cancellationToken);
    }
  }
}
