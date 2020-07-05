using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Users
{
  public interface IUserPermissionRepository: INestedRepositoryBase<UserDoc, UserPermissionDoc>
  {
    Task<UserPermissionDoc> GetAsync(ObjectId userId, CancellationToken cancellationToken);
    Task SetKindAsync(ObjectId userId, PermissionKind? permissionKind, CancellationToken cancellationToken);
  }
}
