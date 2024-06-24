namespace ThingsLibrary.Schema.Converters
{
    /// <summary>
    /// Converts List of item type attributes into a dictionary
    /// </summary>
    public class AttributeValueConverter : JsonConverter<Dictionary<string, ItemTypeAttributeValueSchema>>
    {
        public override Dictionary<string, ItemTypeAttributeValueSchema> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
            if (list == null) { return new(); }

            return list.ToDictionary(x => x.Key, x => new ItemTypeAttributeValueSchema(x.Key, x.Value));
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, ItemTypeAttributeValueSchema> values, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Name)));
        }
    }
}
