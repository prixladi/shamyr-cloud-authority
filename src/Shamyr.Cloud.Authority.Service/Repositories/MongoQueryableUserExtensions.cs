using System;
using System.Linq;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public static partial class MongoQueryableUserExtensions
  {
    public static IMongoQueryable<UserDoc> WhereUsernameContains(this IMongoQueryable<UserDoc> query, string? username)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (username is null)
        return query;

      string normalizedUsername = username.Normalize();

      return query.Where(doc => doc.NormalizedUsername.Contains(normalizedUsername));
    }

    public static IMongoQueryable<UserDoc> WhereEmailContains(this IMongoQueryable<UserDoc> query, string? email)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (email is null)
        return query;

      string normalizedEmail = email.Normalize();

      return query.Where(doc => doc.NormalizedEmail.Contains(normalizedEmail));
    }

    public static IMongoQueryable<UserDoc> WhereAdmin(this IMongoQueryable<UserDoc> query, bool? admin)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (admin is null)
        return query;

      return query.Where(doc => doc.Admin == admin.Value);
    }
  }
}
