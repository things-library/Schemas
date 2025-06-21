// ================================================================================
// <copyright file="LibraryItemTypeAttachment.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item type attachment
    /// </summary>    
    [DebuggerDisplay("{Type} ({Name}, {Type})")]
    public class LibraryItemTypeAttachmentDto : Base.SchemaBase
    {
        /// <summary>
        /// Attachment Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

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

        #region --- Extended ---

        public LibraryItemTypeDto? ItemType { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between things and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryItemTypeDto parent)
        {
            // fix all of the reference variables
            this.ItemType = parent;
        }

        #endregion

        /// <summary>
        /// Item Type Attachment
        /// </summary>
        public LibraryItemTypeAttachmentDto()
        {
            //nothing
        }
    }
}

