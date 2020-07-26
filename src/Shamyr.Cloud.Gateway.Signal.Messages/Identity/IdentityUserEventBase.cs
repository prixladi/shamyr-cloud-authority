using System;
using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public abstract class IdentityUserEventBase: IdentityEventBase
  {
    [JsonIgnore]
    public abstract string Resource { get; }

    public string UserId { get; set; }

    protected IdentityUserEventBase(string userId, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    protected IdentityUserEventBase(string userId, IOperationContext context)
      : base(context)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }
  }
}
