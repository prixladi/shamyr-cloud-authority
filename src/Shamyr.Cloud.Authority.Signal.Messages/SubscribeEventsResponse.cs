using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class SubscribeEventsResponse: EventBase
  {
    [JsonConstructor]
    public SubscribeEventsResponse(string scopeId, string? parentScopeId)
      : base(scopeId, parentScopeId) { }

    public SubscribeEventsResponse(ILoggingContext context)
      : base(context) { }
  }
}
