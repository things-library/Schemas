// ================================================================================
// <copyright file="LibraryItemTypeTagsConverter.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Converters
{
    /// <summary>
    /// Converts List of item type attributes into a dictionary
    /// </summary>
    public class LibraryItemTypeTagsConverter : JsonConverter<IDictionary<string, LibraryItemTypeTagValueDto>>
    {
        public override Dictionary<string, LibraryItemTypeTagValueDto> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
            if (list == null) { return new(); }

            return list.ToDictionary(x => x.Key, x => new LibraryItemTypeTagValueDto(x.Key, x.Value));
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<string, LibraryItemTypeTagValueDto> values, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(values.ToDictionary(x => x.Key, x => x.Value.Name)));
        }
    }
}