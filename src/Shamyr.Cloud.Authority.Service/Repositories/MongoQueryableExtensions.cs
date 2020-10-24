using System;
using MongoDB.Driver.Linq;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public static partial class MongoQueryableExtensions
  {
    public static IMongoQueryable<TDocument> Pagination<TDocument>(this IMongoQueryable<TDocument> query, IPagination pagination)
    {
      if (pagination is null)
        throw new ArgumentNullException(nameof(pagination));

      return query
        .Skip(pagination.Skip)
        .Take(pagination.Take);
    }
  }
}
