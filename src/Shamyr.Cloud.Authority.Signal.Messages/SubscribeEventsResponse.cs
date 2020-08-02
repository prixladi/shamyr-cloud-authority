using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class SubscribeEventsResponse: EventBase
  {
    [JsonConstructor]
    public SubscribeEventsResponse(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    public SubscribeEventsResponse(IOperationContext context)
      : base(context) { }
  }
}
