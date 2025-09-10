// ================================================================================
// <copyright file="ItemTypeAttachment.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Library.Base;

namespace ThingsLibrary.Schema.Library
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
        /// Where in the list should this item show up priority wise?
        /// </summary>        
        [JsonPropertyName("seq"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Sequence Order"), Required]
        public short Sequence { get; set; } = 0;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Item Type</param>
        /// <param name="name">Name</param>
        /// <exception cref="ArgumentException">When invalid type</exception>
        public ItemTypeAttachmentDto(string type, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(type);
            ArgumentException.ThrowIfNullOrEmpty(name);
            if (!SchemaBase.IsKeyValid(type)) { throw new ArgumentException($"Invalid type '{type}"); }

            this.Type = type;
            this.Name = name;
        }
    }
}

