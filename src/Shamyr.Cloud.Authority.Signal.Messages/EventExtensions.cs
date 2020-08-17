using System;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public static class EventExtensions
  {
    public static ILoggingContext GetContext(this EventBase message)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      return new LoggingContext(message.ScopeId, message.ParentScopeId);
    }

    public static void ChangeContext(this EventBase message, ILoggingContext context)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));
      if (context is null)  
        throw new ArgumentNullException(nameof(context));

      if (context.ScopeId != message.ScopeId)
        throw new ArgumentException("Unable to change contex, because it has different correlation id than message.", nameof(context));

      message.ParentScopeId = context.ParentScopeId;
    }
  }
}
