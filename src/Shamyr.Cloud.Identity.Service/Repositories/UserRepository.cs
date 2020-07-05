using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Identity.Service.Repositories
{
  public class UserRepository: RepositoryBase<UserDoc>, IUserRepository
  {
    public UserRepository(IDatabaseContext dbContext)
      : base(dbContext) { }
  }
}
