namespace ThingsLibrary.Schema.Library.Converters
{
    public sealed class AttributeDataTypeConverter : JsonConverter<AttributeDataTypeDto>
    {
        public override AttributeDataTypeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var key = JsonSerializer.Deserialize<string>(ref reader, options);
            if (string.IsNullOrWhiteSpace(key)) { return AttributeDataTypes.Items[AttributeDataTypes.String]; }

            if(AttributeDataTypes.Items.TryGetValue(key, out var attributeDataType))
            {
                return attributeDataType;
            }
            else
            {
                throw new ArgumentException($"Unexpected attribute data type '{key}'.");
            }
        }

        public override void Write(Utf8JsonWriter writer, AttributeDataTypeDto attribute, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attribute.Key));
        }            
    }
}
