using System;
using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public class SubscribeIdentityResourcesRequest: MessageBase
  {
    public string[] Resources { get; }

    [JsonConstructor]
    public SubscribeIdentityResourcesRequest(string[] resources, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }

    public SubscribeIdentityResourcesRequest(string[] resources, IOperationContext context)
      : base(context)
    {
      Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }
  }
}
