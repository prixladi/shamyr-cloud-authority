using System;
using System.Text.Json.Serialization;
using Shamyr.Tracking;

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

    public LoginRequest(string clientId, string clientSecret, IOperationContext context)
      : base(context)
    {
      ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
      ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    }
  }
}
