﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Shamyr.Cloud.Bson
{
  public static class NewtonsoftJsonSerializerOptions
  {
    public static JsonSerializerSettings WithObjectIdConveter
    {
      get
      {
        var options = new JsonSerializerSettings()
        {
          TypeNameHandling = TypeNameHandling.Auto,
          ContractResolver = new CamelCasePropertyNamesContractResolver(),
          Converters = { new NewtonsoftObjectIdJsonConverter() }
        };

        return options;
      }
    }
  }
}
