// ================================================================================
// <copyright file="RootItem.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Library.Interfaces;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Root Item - Designed to be the serialized root
    /// </summary>
    [DebuggerDisplay("Name: {Name}, Type: {Type})")]
    public class RootItemDto : ItemDto, IRootItemDto
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

        /// <summary>
        /// Constructor
        /// </summary>
        public RootItemDto() : base()
        {
            //nothing            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RootItemDto(string type, string name, string key) : base(type, name)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            this.Key = key;
        }


        /// <summary>
        /// Get the name from the definitions doc
        /// </summary>        
        /// <param name="typeKey">Type</param>        
        /// <returns></returns>
        public string GetTypeName(string typeKey)
        {
            var type = this.Types.FirstOrDefault(x => x.Key == typeKey);
            if (type.Value == null) { return string.Empty; }

            return type.Value.Name;
        }

        /// <summary>
        /// Get the tag name from the definitions doc
        /// </summary>        
        /// <param name="typeKey">Type</param>
        /// <param name="tagKey">Tag Name</param>
        /// <returns></returns>
        public string GetTypeTagName(string typeKey, string tagKey)
        {
            var type = this.Types.FirstOrDefault(x => x.Key == typeKey);
            if (type.Value == null) { return string.Empty; }

            var typeTag = type.Value.Tags.FirstOrDefault(x => x.Key == tagKey);
            if (typeTag.Value == null) { return string.Empty; }

            return typeTag.Value.Name;
        }
    }
}
