using System;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public abstract class MessageBase
  {
    public string OperationId { get; }

    public string? ParentOperationId { get; internal set; }

    protected MessageBase(string operationId, string? parentOperationId)
    {
      OperationId = operationId ?? throw new ArgumentNullException(nameof(operationId));
      ParentOperationId = parentOperationId;
    }

    protected MessageBase(IOperationContext context)
    {
      OperationId = context.Id;
      ParentOperationId = context.ParentId;
    }
  }
}
