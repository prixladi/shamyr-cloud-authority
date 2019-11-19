using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Repositories;

namespace Shamyr.Server.Services
{
  public abstract class ServiceBase<TDocument, TRepository>: IServiceBase<TDocument>
    where TDocument : DocumentBase
    where TRepository : IRepositoryBase<TDocument>
  {
    protected readonly TRepository fRepository;

    protected ServiceBase(TRepository repository)
    {
      fRepository = repository;
    }

    public Task<long> CountAsync(CancellationToken cancellationToken)
    {
      return fRepository.CountAsync(cancellationToken);
    }

    public Task<bool> ExistsByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
      return fRepository.ExistsByIdAsync(id, cancellationToken);
    }

    public Task CreateAsync(TDocument document, CancellationToken cancellationToken)
    {
      return fRepository.InsertOneAsync(document, cancellationToken);
    }

    public Task<TDocument?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
      return fRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<List<TDocument>> GetPageAsync(int count, int offset, CancellationToken cancellationToken)
    {
      if (count < 1)
        throw new ArgumentOutOfRangeException(nameof(count));

      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof(offset));

      return fRepository.GetPageAsync(count, offset, cancellationToken);
    }
  }
}
