using Shamyr.Database.Mongo;

namespace Shamyr.Server.Services
{
  public interface INestedServiceBase<TDocument, TNested>
    where TDocument : DocumentBase
  {
  }
}