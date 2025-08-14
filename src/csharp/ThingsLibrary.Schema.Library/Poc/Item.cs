// ================================================================================
// <copyright file="Item.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Poc
{
    /// <summary>
    /// Item Schema - Flexible
    /// </summary>
    [DebuggerDisplay("Name: {Name}, Type: {Type})")]
    public class ItemDto
    {   
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? Date { get; set; }
                
        /// <summary>
        /// Item Type - describes what type of item we are talking about.
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("tags"), JsonIgnoreEmptyCollection]
        [Display(Name = "Tags")]
        public IDictionary<string, ItemTagDto> Tags { get; set; } = new Dictionary<string, ItemTagDto>();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        [Display(Name = "Items")]
        public IDictionary<string, ItemDto> Items { get; set; } = new Dictionary<string, ItemDto>();

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemDto(string type, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(type);
            ArgumentException.ThrowIfNullOrEmpty(name);

            Type = type;
            Name = name;
        }


        /// <summary>
        /// Easy lookup and empty string lookup
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get 
            {
                if(!this.Tags.ContainsKey(key)) { return string.Empty; }

                return this.Tags[key].Value;
            }
        }
    }
}
