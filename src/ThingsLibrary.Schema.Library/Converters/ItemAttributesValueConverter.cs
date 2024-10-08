﻿// ================================================================================
// <copyright file="ItemAttributesValueConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Converters
{
    public class ItemAttributesConverter : JsonConverter<ItemAttributesDto>
    {
        public override ItemAttributesDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            if (list == null) { return new(); }

            var attributes = new ItemAttributesDto();
                        
            foreach (var item in list)
            {
                if (item.Value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.Array)
                    {
                        var values = element.Deserialize<List<object>>() ?? new();
                        foreach (var value in values)
                        {
                            var itemValue = this.GetValue((JsonElement)value);

                            attributes.Add(item.Key, itemValue);
                        }
                    }   
                    else
                    {
                        var itemValue = GetValue(element);
                        attributes.Add(item.Key, itemValue);
                    }
                }
                else
                {
                    attributes.Add(item.Key, $"{item.Value}");
                }
            }

            return attributes;
        }

        private string GetValue(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.String)
            {
                return element.Deserialize<string>() ?? string.Empty;                
            }            
            else if (element.ValueKind == JsonValueKind.True)
            {
                return "true";
            }
            else if (element.ValueKind == JsonValueKind.False)
            {
                return "false";
            }
            else if (element.ValueKind == JsonValueKind.Null)
            {
                return "";
            }
            else if (element.ValueKind == JsonValueKind.Number)
            {
                var number = element.Deserialize<double>();
                return $"{number}";
            }
            else
            {
                throw new ArgumentException("Invalid element type: {}.  Expecting string or array of strings");
            }
        }

        public override void Write(Utf8JsonWriter writer, ItemAttributesDto attributes, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attributes.ToDictionary()));
        }
    }
}
