using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Service.Extensions
{
  public static class UserPermissionDocExtenisons
  {
    public static UserPermissionModel ToModel(this UserPermissionDoc userPermission)
    {
      if (userPermission is null)
        throw new ArgumentNullException(nameof(userPermission));

      return new UserPermissionModel
      {
        Kind = userPermission.Kind
      };
    }
  }
}
