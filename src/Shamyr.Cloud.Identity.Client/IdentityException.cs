using System;

namespace Shamyr.Cloud.Identity.Client
{
  public sealed class IdentityException: Exception
  {
    public int StatusCode { get; private set; }

    public IdentityException(string message, int statusCode, Exception inner)
      : base($"{message} - Status code: {statusCode}", inner)
    {
      StatusCode = statusCode;
    }


    public IdentityException(string message, int statusCode, string? reason)
      : base($"{message} - Status code: {statusCode} - {reason}")
    {
      StatusCode = statusCode;
    }

    public IdentityException(string message, int statusCode)
      : base(message)
    {
      StatusCode = statusCode;
    }
  }
}
