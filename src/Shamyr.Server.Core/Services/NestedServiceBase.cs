using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Repositories;

namespace Shamyr.Server.Services
{
  public class NestedServiceBase<TDocument, TNested, TRepository>: INestedServiceBase<TDocument, TNested>
    where TDocument : DocumentBase
    where TRepository : INestedRepositoryBase<TDocument, TNested>
  {
    protected readonly TRepository fRepository;

    public NestedServiceBase(TRepository repository)
    {
      fRepository = repository;
    }
  }
}
