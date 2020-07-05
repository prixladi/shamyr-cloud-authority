using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Identity.Service.Repositories
{
  public class ClientRepository: RepositoryBase<ClientDoc>, IClientRepository
  {
    public ClientRepository(IDatabaseContext dbContext)
      : base(dbContext) { }
  }
}
