// ================================================================================
// <copyright file="AttributeDataTypeConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Converters
{
    public sealed class AttributeDataTypeConverter : JsonConverter<TagDataTypeDto>
    {
        public override TagDataTypeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var key = JsonSerializer.Deserialize<string>(ref reader, options);
            if (string.IsNullOrWhiteSpace(key)) { return TagDataTypes.Items[TagDataTypes.String]; }

            if(TagDataTypes.Items.TryGetValue(key, out var attributeDataType))
            {
                return attributeDataType;
            }
            else
            {
                throw new ArgumentException($"Unexpected attribute data type '{key}'.");
            }
        }

        public override void Write(Utf8JsonWriter writer, TagDataTypeDto attribute, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attribute.Key));
        }            
    }
}
