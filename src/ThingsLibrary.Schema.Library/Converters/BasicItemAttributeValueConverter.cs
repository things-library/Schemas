namespace ThingsLibrary.Schema.Library.Converters
{
    public class BasicItemAttributesConverter : JsonConverter<BasicItemAttributesDto>
    {
        public override BasicItemAttributesDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            if (list == null) { return new(); }

            var attributes = new BasicItemAttributesDto();
                        
            foreach (var item in list)
            {
                if (item.Value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.String)
                    {
                        var value = element.Deserialize<string>() ?? string.Empty;
                        attributes.Add(item.Key, value);
                    }
                    else if (element.ValueKind == JsonValueKind.Array)
                    {
                        var values = element.Deserialize<List<string>>() ?? new();
                        attributes.Add(item.Key, values);
                    }
                }
                else
                {
                    attributes.Add(item.Key, $"{item.Value}");
                }
            }

            return attributes;
        }

        public override void Write(Utf8JsonWriter writer, BasicItemAttributesDto attributes, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attributes.ToDictionary()));
        }
    }
}
