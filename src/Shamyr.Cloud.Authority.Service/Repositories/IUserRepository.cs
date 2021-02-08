using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Dtos.Users;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB.Repositories;
using Shamyr.MongoDB.Sorting;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public interface IUserRepository: IRepositoryBase<UserDoc>
  {
    Task LogoutAsync(ObjectId id, CancellationToken cancellationToken);
    Task SetSecretAsync(ObjectId id, SecretDoc secret, CancellationToken cancellationToken);
    Task<bool> TrySetDisabledAsync(ObjectId id, bool disabled, CancellationToken cancellationToken);
    Task<bool> TrySetAdminAsync(ObjectId id, bool admin, CancellationToken cancellationToken);
    Task<bool> TryUnsetEmailTokenAndSetVerifiedAsync(ObjectId id, string emailToken, CancellationToken cancellationToken);
    Task<bool> TryAddSecretAsync(ObjectId id, SecretDoc secret, CancellationToken cancellationToken);
    Task UpdateAsync(ObjectId userId, UpdateDto updateDto, CancellationToken cancellationToken);

    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserDoc?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<UserDoc?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserDoc?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<UserDoc?> SetPasswordTokenAsync(string email, string passwordToken, CancellationToken cancellationToken);
    Task<UserDoc?> GetByEmailAndVerifyAsync(string email, CancellationToken cancellationToken);

    Task<List<UserDoc>> GetAsync(FilterDto filter, OrderDefinition<UserDoc>? sort, CancellationToken cancellationToken);
    Task<int> GetUserCountAsync(FilterDto filter, CancellationToken cancellationToken);
  }
}