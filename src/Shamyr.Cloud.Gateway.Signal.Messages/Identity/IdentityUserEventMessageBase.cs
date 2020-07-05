using System;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public abstract class IdentityUserEventMessageBase: IdentityEventMessageBase
  {
    public string UserId { get; set; }

    protected IdentityUserEventMessageBase(string userId, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    protected IdentityUserEventMessageBase(string userId, IOperationContext context)
      : base(context)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }
  }
}
