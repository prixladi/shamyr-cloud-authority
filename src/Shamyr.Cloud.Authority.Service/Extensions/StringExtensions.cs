using System;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class StringExtensions
  {
    public static string Normalize(this string str)
    {
      if (str is null)
        throw new ArgumentNullException(nameof(str));

      return str.ToLower();
    }
  }
}
