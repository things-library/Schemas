namespace ThingsLibrary.Schema.Library.Converters
{
    /// <summary>
    /// Converts List of item type attributes into a dictionary
    /// </summary>
    public class LibraryItemTypeAttributeValueConverter : JsonConverter<Dictionary<string, LibraryItemTypeAttributeValueDto>>
    {
        public override Dictionary<string, LibraryItemTypeAttributeValueDto> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
            if (list == null) { return new(); }

            return list.ToDictionary(x => x.Key, x => new LibraryItemTypeAttributeValueDto(x.Key, x.Value));
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, LibraryItemTypeAttributeValueDto> values, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Name)));
        }
    }
}
