using System;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public static class MessageExtensions
  {
    public static IOperationContext GetContext(this MessageBase message)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      return new OperationContext(message.OperationId, message.ParentOperationId);
    }

    public static void ChangeContext(this MessageBase message, IOperationContext context)
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
