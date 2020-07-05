using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public class SubscribeIdentityResourcesResponse: MessageBase
  {
    [JsonConstructor]
    public SubscribeIdentityResourcesResponse(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    public SubscribeIdentityResourcesResponse(IOperationContext context)
      : base(context) { }
  }
}
