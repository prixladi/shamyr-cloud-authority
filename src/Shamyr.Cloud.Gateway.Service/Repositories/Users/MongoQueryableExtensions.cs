using System;
using System.Linq;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Users
{
  public static class MongoQueryableExtensions
  {
    public static IMongoQueryable<UserDoc> WhereUsernameContains(this IMongoQueryable<UserDoc> query, string? username)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (username is null)
        return query;

      string normalizedUsername = username.ToLower();

      return query.Where(doc => doc.NormalizedUsername.Contains(normalizedUsername));
    }

    public static IMongoQueryable<UserDoc> WhereEmailContains(this IMongoQueryable<UserDoc> query, string? email)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (email is null)
        return query;

      string normalizedEmail = email.ToLower();

      return query.Where(doc => doc.NormalizedEmail.Contains(normalizedEmail));
    }

    public static IMongoQueryable<UserDoc> WhereUserPermission(this IMongoQueryable<UserDoc> query, PermissionKind?[]? kinds)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (kinds is null)
        return query;

      bool containsNull = kinds.Any(x => !x.HasValue);
      var kindsNotNull = kinds.Where(x => x.HasValue).Select(x => (int)x!);

      if (containsNull)
        return query.Where(doc => doc.UserPermission == null || kindsNotNull.Contains((int)doc.UserPermission.Kind));

      return query.Where(doc => kindsNotNull.Contains((int)doc.UserPermission!.Kind));
    }
  }
}
