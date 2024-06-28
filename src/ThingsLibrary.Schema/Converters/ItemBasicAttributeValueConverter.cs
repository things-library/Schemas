namespace ThingsLibrary.Schema.Converters
{
    /// <summary>
    /// Converts List of item type attributes into a dictionary
    /// </summary>
    public class ItemBasicAttributeConverter : JsonConverter<Dictionary<string, ItemBasicAttributeSchema>>
    {
        public override Dictionary<string, ItemBasicAttributeSchema> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            if (list == null) { return new(); }

            var items = new Dictionary<string, ItemBasicAttributeSchema>(list.Count);
            foreach (var item in list)
            {                
                if (item.Value is JsonElement element)
                {
                    if(element.ValueKind == JsonValueKind.String)
                    {
                        var value = element.Deserialize<string>() ?? string.Empty;
                        items[item.Key] = new ItemBasicAttributeSchema(item.Key, value);
                    }
                    else if(element.ValueKind == JsonValueKind.Array)
                    {
                        var values = element.Deserialize<List<string>>() ?? new();
                        items[item.Key] = new ItemBasicAttributeSchema(item.Key, values);
                    }
                }
                else
                {
                    items[item.Key] = new ItemBasicAttributeSchema(item.Key, $"{item.Value}");
                }
            }

            return items;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, ItemBasicAttributeSchema> values, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Values)));
        }
    }
}
