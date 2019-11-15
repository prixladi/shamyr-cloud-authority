using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Repositories;

namespace Shamyr.Server.Services
{
  public class ProjectedServiceBase<TDocument, TProjected, TRepository>: IProjectedServiceBase<TDocument, TProjected>
    where TDocument : DocumentBase
    where TRepository : IProjectedRepositoryBase<TDocument, TProjected>
  {
    protected readonly TRepository fRepository;

    public ProjectedServiceBase(TRepository fRepository)
    {
      this.fRepository = fRepository;
    }
  }
}
