using Shamyr.Database.Mongo;

namespace Shamyr.Server.Services
{
  public interface IProjectedServiceBase<TDocument, TProjected>
    where TDocument : DocumentBase
  {
  }
}