using System;
using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class SubscribeEventsRequest: EventBase
  {
    public string[] Resources { get; }

    [JsonConstructor]
    public SubscribeEventsRequest(string[] resources, string scopeId, string? parentScopeId)
      : base(scopeId, parentScopeId)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }

    public SubscribeEventsRequest(string[] resources, ILoggingContext context)
      : base(context)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }
  }
}
