using Shamyr.Security;

namespace Shamyr.Server.Services
{
  public interface ISecretService
  {
    bool ComparePasswords(string password, Secret userSecret);
    Secret CreateSecret(string password);
  }
}
