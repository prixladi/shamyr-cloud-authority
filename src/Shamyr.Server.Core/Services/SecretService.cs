using System;
using Shamyr.Security;

namespace Shamyr.Server.Services
{
  public class SecretService: ISecretService
  {
    private const int _IterationCount = 10000;
    private const int _HashSize = 32;
    private const int _SaltSize = 16;

    public bool ComparePasswords(string password, Secret secret)
    {
      if (secret is null)
        throw new ArgumentNullException(nameof(secret));

      return SecretUtils.CompareSecret(password, secret, _IterationCount, _HashSize);
    }

    public Secret CreateSecret(string password)
    {
      if (password is null)
        throw new ArgumentNullException(nameof(password));

      return SecretUtils.CreateSecret(password, _SaltSize, _IterationCount, _HashSize);
    }
  }
}
