using System;
using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class SubscribeEventsRequest: EventBase
  {
    public string[] Resources { get; }

    [JsonConstructor]
    public SubscribeEventsRequest(string[] resources, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }

    public SubscribeEventsRequest(string[] resources, IOperationContext context)
      : base(context)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }
  }
}
