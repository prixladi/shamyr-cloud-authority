using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace Shamyr.Server.Binders
{
  public class ObjectIdBinderProvider: IModelBinderProvider
  {
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));

      if (context.Metadata.ModelType == typeof(ObjectId)
          || context.Metadata.ModelType == typeof(ObjectId?))
        return new ObjectIdBinder();

      // TODO: Find out right solution for this one
      return default!;
    }
  }
}
