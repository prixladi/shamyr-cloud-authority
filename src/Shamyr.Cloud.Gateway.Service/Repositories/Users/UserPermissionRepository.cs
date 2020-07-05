using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Users
{
  public class UserPermissionRepository: NestedRepositoryBase<UserDoc, UserPermissionDoc>, IUserPermissionRepository
  {
    protected override Expression<Func<UserDoc, UserPermissionDoc>> Map => doc => doc.UserPermission!;

    public UserPermissionRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<UserPermissionDoc> GetAsync(ObjectId userId, CancellationToken cancellationToken)
    {
      return await GetByDocumentIdAsync(userId, cancellationToken);
    }

    public async Task SetKindAsync(ObjectId userId, PermissionKind? permissionKind, CancellationToken cancellationToken)
    {
      if (permissionKind.HasValue)
        await SetByDocumentIdAsync(userId, new UserPermissionDoc { Kind = permissionKind.Value }, cancellationToken);
      else
        await UnsetByDocumentIdAsync(userId, cancellationToken);
    }
  }
}
