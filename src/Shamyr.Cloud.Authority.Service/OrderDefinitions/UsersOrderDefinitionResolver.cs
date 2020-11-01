using System;
using System.Linq.Expressions;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB.Sorting;

namespace Shamyr.Cloud.Authority.Service.OrderDefinitions
{
  public static class UsersOrderDefinitionResolver
  {
    public static OrderDefinition<UserDoc>? FromModel(SortModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      Expression<Func<UserDoc, object>>? orderBy = null;

      switch (model.OrderBy)
      {
        case null:
          return null;
        case SortTypes.Username:
          orderBy = x => x.NormalizedUsername!;
          break;
        case SortTypes.Email:
          orderBy = x => x.NormalizedEmail;
          break;
        default:
          throw new InvalidOperationException($"Undefined case for users sort {model.OrderBy}");
      }

      return new OrderDefinition<UserDoc>(orderBy, model.Ascending);
    }
  }
}
