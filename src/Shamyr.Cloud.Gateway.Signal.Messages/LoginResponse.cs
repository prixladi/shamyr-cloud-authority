using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public class LoginResponse: MessageBase
  {
    [JsonConstructor]
    public LoginResponse(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    public LoginResponse(IOperationContext context)
      : base(context) { }
  }
}
