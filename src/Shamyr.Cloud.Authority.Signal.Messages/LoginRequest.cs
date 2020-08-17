using System;
using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class LoginRequest: EventBase
  {
    public string ClientId { get; }
    public string ClientSecret { get; }

    [JsonConstructor]
    public LoginRequest(string clientId, string clientSecret, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
      ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    }

    public LoginRequest(string clientId, string clientSecret, ILoggingContext context)
      : base(context)
    {
      ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
      ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    }
  }
}
