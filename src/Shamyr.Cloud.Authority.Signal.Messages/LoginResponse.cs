using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class LoginResponse: EventBase
  {
    [JsonConstructor]
    public LoginResponse(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    public LoginResponse(ILoggingContext context)
      : base(context) { }
  }
}
