using System.Collections.Generic;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Authorization
{
  internal static class UserPolicy
  {
    public const string _View = "View";
    public const string _Control = "Control";
    public const string _Configure = "Configure";
    public const string _Own = "Own";

    public static IEnumerable<string> GetRequiredRoles(PermissionKind? permission)
    {
      switch (permission)
      {
        case PermissionKind.View:
          yield return _View;
          break;
        case PermissionKind.Control:
          yield return _Control;
          break;
        case PermissionKind.Configure:
          yield return _Configure;
          break;
        case PermissionKind.Own:
          yield return _Own;
          break;
      }
    }

    public static IEnumerable<string> GetRoles(PermissionKind? permission)
    {
      switch (permission)
      {
        case PermissionKind.View:
          yield return _View;
          break;
        case PermissionKind.Control:
          yield return _View;
          yield return _Control;
          break;
        case PermissionKind.Configure:
          yield return _View;
          yield return _Control;
          yield return _Configure;
          break;
        case PermissionKind.Own:
          yield return _View;
          yield return _Control;
          yield return _Configure;
          yield return _Own;
          break;
      }
    }
  }
}
