using System;
using System.Security.Cryptography;
using Shamyr.Security;

namespace Shamyr.Server.Services
{
  public class SecretService: ISecretService
  {
    private const int _IterationCount = 10000;
    private const int _SaltSize = 16;

    private static readonly HashAlgorithmName fHashAlgorithm = HashAlgorithmName.SHA256;

    public bool ComparePasswords(string password, Secret secret)
    {
      if (secret is null)
        throw new ArgumentNullException(nameof(secret));

      return SecretUtils.CompareSecret(password, secret, fHashAlgorithm, _IterationCount);
    }

    public Secret CreateSecret(string password)
    {
      if (password is null)
        throw new ArgumentNullException(nameof(password));

      return SecretUtils.CreateSecret(password, fHashAlgorithm, _SaltSize, _IterationCount);
    }
  }
}
