// ================================================================================
// <copyright file="ItemType.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item type
    /// </summary>    
    [DebuggerDisplay("{Name}")]
    public class ItemTypeDto
    {
        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Description"), StringLength(50, MinimumLength = 1)]
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("tags"), JsonIgnoreEmptyCollection]
        public IDictionary<string, ItemTypeTagDto> Tags { get; set; } = new Dictionary<string, ItemTypeTagDto>();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        public List<ItemTypeAttachmentDto> Items { get; set; } = new List<ItemTypeAttachmentDto>();

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTypeDto()
        {
            //nothing
        }
    }
}

