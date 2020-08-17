using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public abstract class EventMessageBase: EventBase
  {
    protected EventMessageBase(string scopeId, string? parentScopeId)
      : base(scopeId, parentScopeId) { }

    protected EventMessageBase(ILoggingContext context)
      : base(context) { }
  }
}
