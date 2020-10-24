using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class LoginResponse: EventBase
  {
    [JsonConstructor]
    public LoginResponse(string scopeId, string? parentScopeId)
      : base(scopeId, parentScopeId) { }

    public LoginResponse(ILoggingContext context)
      : base(context) { }
  }
}
