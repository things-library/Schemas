namespace ThingsLibrary.Schema.Library.Converters
{
    /// <summary>
    /// Converts List of item type attributes into a dictionary
    /// </summary>
    public class LibraryItemAttributeConverter : JsonConverter<Dictionary<string, LibraryItemAttributeDto>>
    {
        public override Dictionary<string, LibraryItemAttributeDto> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            if (list == null) { return new(); }

            var items = new Dictionary<string, LibraryItemAttributeDto>(list.Count);
            foreach (var item in list)
            {                
                if (item.Value is JsonElement element)
                {
                    if(element.ValueKind == JsonValueKind.String)
                    {
                        var value = element.Deserialize<string>() ?? string.Empty;
                        items[item.Key] = new LibraryItemAttributeDto(item.Key, value);
                    }
                    else if(element.ValueKind == JsonValueKind.Array)
                    {
                        var values = element.Deserialize<List<string>>() ?? new();
                        items[item.Key] = new LibraryItemAttributeDto(item.Key, values);
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected JsonElement type converting Item Attribute value");
                    }
                }
                else
                {
                    items[item.Key] = new LibraryItemAttributeDto(item.Key, $"{item.Value}");
                }
            }

            return items;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, LibraryItemAttributeDto> values, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Values)));
        }
    }
}
