// ================================================================================
// <copyright file="ItemTypeAttachment.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Item type attachment
    /// </summary>    
    [DebuggerDisplay("{Type} ({Name}, {Type})")]
    public class ItemTypeAttachmentDto
    {
        /// <summary>
        /// Item Type
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name (Override)
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
        /// Where in the list should this item show up priority wise?
        /// </summary>        
        [JsonPropertyName("weight"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Display Order (Weight)"), DefaultValue(0), Required]
        public short Weight { get; set; } = 0;

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Item Type Attachment
        /// </summary>
        public ItemTypeAttachmentDto()
        {
            //nothing
        }
    }
}

