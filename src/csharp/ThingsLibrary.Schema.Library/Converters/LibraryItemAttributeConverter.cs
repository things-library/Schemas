// ================================================================================
// <copyright file="LibraryItemAttributeConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

//            var items = new Dictionary<string, LibraryItemAttributeDto>(list.Count);
//            foreach (var item in list)
//            {                
//                if (item.Value is JsonElement element)
//                {
//                    if(element.ValueKind == JsonValueKind.String)
//                    {
//                        var value = element.Deserialize<string>() ?? string.Empty;
//                        items[item.Key] = new LibraryItemAttributeDto(item.Key, value);
//                    }
//                    else if(element.ValueKind == JsonValueKind.Array)
//                    {
//                        var values = element.Deserialize<List<string>>() ?? new();
//                        items[item.Key] = new LibraryItemAttributeDto(item.Key, values);
//                    }
//                    else
//                    {
//                        throw new ArgumentException("Unexpected JsonElement type converting Item Attribute value");
//                    }
//                }
//                else
//                {
//                    items[item.Key] = new LibraryItemAttributeDto(item.Key, $"{item.Value}");
//                }
//            }

//            return items;
//        }

//        public override void Write(Utf8JsonWriter writer, IDictionary<string, LibraryItemAttributeDto> values, JsonSerializerOptions options)
//        {
//            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Values)));
//        }
//    }
//}
