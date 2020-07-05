using System;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Gateway.Service.Models;

namespace Shamyr.Cloud.Gateway.Service.Repositories
{
  public static class Extensions
  {
    public static IMongoQueryable<TDocument> Pagination<TDocument>(
      this IMongoQueryable<TDocument> query,
      PaginationModel pagination)
    {
      if (pagination is null)
        throw new ArgumentNullException(nameof(pagination));

      return query
        .Skip(pagination.Skip)
        .Take(pagination.Take);
    }
  }
}
