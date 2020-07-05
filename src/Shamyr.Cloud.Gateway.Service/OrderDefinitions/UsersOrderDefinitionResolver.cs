using System;
using System.Linq.Expressions;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.MongoDB.Sorting;

namespace Shamyr.Cloud.Gateway.Service.OrderDefinitions
{
  public static class UsersOrderDefinitionResolver
  {
    public static OrderDefinition<UserDoc>? FromModel(UserSortModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      Expression<Func<UserDoc, object>>? orderBy = null;

      switch (model.OrderBy)
      {
        case null:
          return null;
        case UserSortTypes.Username:
          orderBy = x => x.NormalizedUsername;
          break;
        case UserSortTypes.Email:
          orderBy = x => x.NormalizedEmail;
          break;
        case UserSortTypes.UserPermission:
          orderBy = x => x.UserPermission!.Kind;
          break;
        default:
          throw new InvalidOperationException($"Undefined case for users sort {model.OrderBy}");
      }

      return new OrderDefinition<UserDoc>(orderBy, model.Ascending);
    }
  }
}
