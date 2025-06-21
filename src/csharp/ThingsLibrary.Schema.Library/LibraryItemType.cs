// ================================================================================
// <copyright file="LibraryItemType.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item type
    /// </summary>    
    [DebuggerDisplay("{Name} ({Key})")]
    public class LibraryItemTypeDto : Base.SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>        
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("tags"), JsonIgnoreEmptyCollection]
        public IDictionary<string, LibraryItemTypeTagDto> Tags { get; set; } = new Dictionary<string, LibraryItemTypeTagDto>();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        public IDictionary<string, LibraryItemTypeAttachmentDto> Items { get; set; } = new Dictionary<string, LibraryItemTypeAttachmentDto>();


        #region --- Extended ---

        public LibraryDto? Library { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between things and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryDto parent)
        {
            this.Library = parent;

            // fix all of the reference variables
            foreach (var pair in this.Tags)
            {
                pair.Value.Key = pair.Key;                
                pair.Value.Init(this);
            }

            foreach (var pair in this.Items)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);                
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryItemTypeDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public LibraryItemTypeDto(string key, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            if (SchemaBase.IsKeyValid(key)) { throw new ArgumentException(SchemaBase.KeyPatternErrorMessage); }

            this.Key = key;
            this.Name = name;
        }
    }
}

