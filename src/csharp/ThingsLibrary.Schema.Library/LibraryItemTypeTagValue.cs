// ================================================================================
// <copyright file="LibraryItemTypeTagValue.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Tag Value
    /// </summary>
    [DebuggerDisplay("{Name} ({Key})")]
    public class LibraryItemTypeTagValueDto : Base.SchemaBase
    {        
        /// <summary>
        /// Tag Unique Key
        /// </summary>  
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Tag Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;


        #region --- Extended ---

        public LibraryItemTypeTagDto? Tag { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between things and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryItemTypeTagDto parent)
        {
            this.Tag = parent;            
        }

        #endregion        

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryItemTypeTagValueDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public LibraryItemTypeTagValueDto(string key, string name)
        {
            this.Key = key;
            this.Name = name;
        }
    }
}
