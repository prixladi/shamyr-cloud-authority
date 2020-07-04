using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Shamyr.Server
{
  public class NewtonsoftObjectIdJsonConverter: JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(ObjectId) || objectType == typeof(ObjectId?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader is null)
        throw new ArgumentNullException(nameof(reader));

      if (reader.TokenType == JsonToken.Null)
        return reader.Value;

      if (reader.TokenType != JsonToken.String)
        throw new InvalidOperationException($"Unexpected token parsing ObjectId. Expected String, got {reader.TokenType}.");

      string value = (string)reader.Value;
      if (ObjectId.TryParse(value, out var objectId))
        return objectId;

      throw new FormatException($"Read string cannot be converted to type '{typeof(ObjectId)}'.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (writer is null)
        throw new ArgumentNullException(nameof(writer));

      if (value is ObjectId objectId)
        writer.WriteValue(objectId.ToString());
      else
        throw new ArgumentException($"Argument is not type of '{typeof(ObjectId)}'.", nameof(value));
    }
  }
}
