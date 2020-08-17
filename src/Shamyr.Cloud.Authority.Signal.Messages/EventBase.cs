using System;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public abstract class EventBase
  {
    public string ScopeId { get; }

    public string? ParentScopeId { get; internal set; }

    protected EventBase(string scopeId, string? parentScopeId)
    {
      ScopeId = scopeId ?? throw new ArgumentNullException(nameof(scopeId));
      ParentScopeId = parentScopeId;
    }

    protected EventBase(ILoggingContext context)
    {
      ScopeId = context.ScopeId;
      ParentScopeId = context.ParentScopeId;
    }
  }
}
