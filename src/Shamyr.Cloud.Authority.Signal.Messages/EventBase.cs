using System;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public abstract class EventBase
  {
    public string OperationId { get; }

    public string? ParentOperationId { get; internal set; }

    protected EventBase(string operationId, string? parentOperationId)
    {
      OperationId = operationId ?? throw new ArgumentNullException(nameof(operationId));
      ParentOperationId = parentOperationId;
    }

    protected EventBase(IOperationContext context)
    {
      OperationId = context.Id;
      ParentOperationId = context.ParentId;
    }
  }
}
