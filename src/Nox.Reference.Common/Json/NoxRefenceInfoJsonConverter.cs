using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nox.Reference.Common;

public class NoxRefenceInfoJsonConverter<TSource, TDest> : JsonConverter<TSource>
    where TDest : TSource
{
    public override TSource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<TDest>(ref reader, options)!;
    }

    public override void Write(Utf8JsonWriter writer, TSource value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value!, options);
    }
}