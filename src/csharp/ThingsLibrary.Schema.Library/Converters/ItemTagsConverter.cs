// ================================================================================
// <copyright file="ItemTagsConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Converters
{
    public class ItemTagsConverter : JsonConverter<ItemTagsDto>
    {
        public override ItemTagsDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            if (list == null) { return new(); }

            var attributes = new ItemTagsDto();
                        
            foreach (var item in list)
            {
                if (item.Value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.Array)
                    {
                        throw new ArgumentException("Arrays not supported in attribute values.");
                    }

                    var itemValue = GetValue(element);
                    attributes.Add(item.Key, itemValue);
                }
                else
                {
                    attributes.Add(item.Key, $"{item.Value}");
                }
            }

            return attributes;
        }

        private object GetValue(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.String)
            {
                return element.Deserialize<string>() ?? string.Empty;
            }
            else if (element.ValueKind == JsonValueKind.True)
            {
                return true;
            }
            else if (element.ValueKind == JsonValueKind.False)
            {
                return false;
            }            
            else if (element.ValueKind == JsonValueKind.Null)
            {
                return "";
            }
            else if (element.ValueKind == JsonValueKind.Number)
            {                
                return element.Deserialize<double>();
            }
            else
            {
                throw new ArgumentException("Invalid element type: {}.  Expecting string or array of strings");
            }
        }

        public override void Write(Utf8JsonWriter writer, ItemTagsDto attributes, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attributes.ToDictionary()));
        }
    }
}
