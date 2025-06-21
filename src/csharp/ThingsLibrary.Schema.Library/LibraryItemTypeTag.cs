// ================================================================================
// <copyright file="LibraryItemTypeTag.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Tags
    /// </summary>
    [DebuggerDisplay("{Name}, ({Key}, Type: {Type}, Units: {Units})")]
    public class LibraryItemTypeTagDto : Base.SchemaBase
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
        /// Tag Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Data Type that this tag represents.. like pick list, date-time, etc
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Data Type"), DefaultValue(TagDataTypes.String), Required]
        [AllowedValues(
            TagDataTypes.Boolean,
            TagDataTypes.Currency,
            TagDataTypes.CurrencyRange,
            TagDataTypes.Date,
            TagDataTypes.DateTime,
            TagDataTypes.Duration,
            TagDataTypes.Email,
            TagDataTypes.Enum,
            TagDataTypes.Html,
            TagDataTypes.Password,
            TagDataTypes.Phone,
            TagDataTypes.String,
            TagDataTypes.TextArea,
            TagDataTypes.Time,
            TagDataTypes.Url,
            TagDataTypes.Decimal,
            TagDataTypes.Integer,
            TagDataTypes.IntegerRange,
            TagDataTypes.DecimalRange
        )]
        public string Type { get; set; } = "string";

        /// <summary>
        /// Unit Symbol
        /// </summary>
        [JsonPropertyName("units"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Unit Symbol"), Required(AllowEmptyStrings = true), DefaultValue(""), MaxLength(20)]
        public string Units { get; set; } = string.Empty;

        /// <summary>
        /// Where in the list should this item show up priority wise?
        /// </summary>        
        [JsonPropertyName("weight"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Display Order (Weight)"), DefaultValue(0), Required]
        public short Weight { get; set; } = 0;

        /// <summary>
        /// Tag Values
        /// </summary>
        /// <remarks>Only used for enum (picklist) tags</remarks>
        [JsonPropertyName("values"), JsonConverter(typeof(LibraryItemTypeTagsConverter)), JsonIgnoreEmptyCollection]
        [ValidateCollectionItems]
        public IDictionary<string, LibraryItemTypeTagValueDto> Values { get; set; } = new Dictionary<string, LibraryItemTypeTagValueDto>();

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
            this.ItemType = parent;

            // fix all of the reference variables
            foreach (var pair in this.Values)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);
            }
        }

        #endregion
    }
}
