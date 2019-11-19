using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Database.Mongo;

namespace Shamyr.Server.Services
{
  public interface IServiceBase<TDocument>
    where TDocument : DocumentBase
  {
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task CreateAsync(TDocument document, CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(ObjectId id, CancellationToken cancellationToken);
    Task<TDocument?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken);
    Task<List<TDocument>> GetPageAsync(int count, int offset, CancellationToken cancellationToken);
  }
}
