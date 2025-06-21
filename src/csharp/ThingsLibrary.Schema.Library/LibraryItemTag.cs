// ================================================================================
// <copyright file="LibraryItemTag.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Tag
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class LibraryItemTagDto : Base.SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Tag Value
        /// </summary>
        [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1)]
        public string Value { get; set; } = string.Empty;

        #region --- Extended ---

        /// <summary>
        /// Item (aka: parent)
        /// </summary>
        [JsonIgnore]
        public LibraryItemDto? Parent { get; set; }

        /// <summary>
        /// Item Type Tag
        /// </summary>
        [JsonIgnore]
        public LibraryItemTypeTagDto? ItemTypeTag { get; set; }

        /// <summary>
        /// Tag Value (if pick list)
        /// </summary>
        [JsonIgnore]
        public LibraryItemTypeTagValueDto? ItemTypeTagValue { get; set; }

        /// <summary>
        /// Tag Values (if pick list)
        /// </summary>
        [JsonIgnore]
        public IDictionary<string, LibraryItemTypeTagValueDto> ItemTypeTagValues { get; set; } = new Dictionary<string, LibraryItemTypeTagValueDto>();

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between items and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryItemDto parent)
        {
            this.Parent = parent;

            LibraryItemTypeTagDto? itemTypeTag;
            if (parent.ItemType?.Tags.TryGetValue(this.Key, out itemTypeTag) == true)
            {
                this.ItemTypeTag = itemTypeTag;
                if (itemTypeTag.Type == "enum")
                {
                    // lookup value if there is one
                    LibraryItemTypeTagValueDto? itemTypeTagValue;
                    if (itemTypeTag.Values.TryGetValue(this.Value, out itemTypeTagValue))
                    {
                        this.ItemTypeTagValue = itemTypeTagValue;
                    }
                    else
                    {
                        throw new ArgumentException($"Unable to find item type tag value '{itemTypeTag.Key}:{this.Key}'");
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Unable to find item type tag '{this.Key}'");
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryItemTagDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public LibraryItemTagDto(string key, string value)
        {
            this.Key = key;
            this.Value = value;            
        }
    }
}
