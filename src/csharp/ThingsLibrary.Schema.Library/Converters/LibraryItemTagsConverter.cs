// ================================================================================
// <copyright file="LibraryItemAttributesConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Converters
{
    public class LibraryItemTagsConverter : JsonConverter<LibraryItemTagsDto>
    {
        public override LibraryItemTagsDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
            if (list == null) { return new(); }

            var attributes = new LibraryItemTagsDto();

            foreach (var item in list)
            {
                attributes.Add(item.Key, $"{item.Value}");
            }

            return attributes;
        }

        public override void Write(Utf8JsonWriter writer, LibraryItemTagsDto attributes, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(attributes.ToDictionary()));
        }
    }
}
