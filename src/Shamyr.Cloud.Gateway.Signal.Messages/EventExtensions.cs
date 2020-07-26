using System;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public static class EventExtensions
  {
    public static IOperationContext GetContext(this EventBase message)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      return new OperationContext(message.OperationId, message.ParentOperationId);
    }

    public static void ChangeContext(this EventBase message, IOperationContext context)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));
      if (context is null)
        throw new ArgumentNullException(nameof(context));

      if (context.Id != message.OperationId)
        throw new ArgumentException("Unable to change contex, because it has different correlation id than message.", nameof(context));

      message.ParentOperationId = context.ParentId;
    }
  }
}
