using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class SubscribeEventsResponse: EventBase
  {
    [JsonConstructor]
    public SubscribeEventsResponse(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    public SubscribeEventsResponse(ILoggingContext context)
      : base(context) { }
  }
}
