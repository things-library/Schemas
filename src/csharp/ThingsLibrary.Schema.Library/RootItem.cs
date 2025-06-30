// ================================================================================
// <copyright file="RootItem.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Root Item - Designed to be the serialized root
    /// </summary>
    [DebuggerDisplay("Name: {Name}, Type: {Type})")]
    public class RootItemDto : ItemDto
    {
        /// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri SchemaUrl { get; set; } = new Uri($"{Base.SchemaBase.SchemaBaseUrl}/library.json");

        /// <summary>
        /// Item Type - describes what type of item we are talking about.
        /// </summary>
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Item Types consumed by this library / branch
        /// </summary>
        [JsonPropertyName("types")]
        public IDictionary<string, ItemTypeDto> Types { get; set; } = new Dictionary<string, ItemTypeDto>();
    }
}
