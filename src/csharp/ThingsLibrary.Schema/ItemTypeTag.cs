// ================================================================================
// <copyright file="ItemTypeTag.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Tags
    /// </summary>
    [DebuggerDisplay("{Name}, ({Key}, Type: {Type}, Units: {Units})")]
    public class ItemTypeTagDto
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
        [Display(Name = "Data Type"), DefaultValue(ItemTagDataTypesDto.String), Required]
        [AllowedValues(
            ItemTagDataTypesDto.Boolean,
            ItemTagDataTypesDto.Currency,
            ItemTagDataTypesDto.CurrencyRange,
            ItemTagDataTypesDto.Date,
            ItemTagDataTypesDto.DateTime,
            ItemTagDataTypesDto.Duration,
            ItemTagDataTypesDto.Email,
            ItemTagDataTypesDto.Enum,
            ItemTagDataTypesDto.Html,
            ItemTagDataTypesDto.Password,
            ItemTagDataTypesDto.Phone,
            ItemTagDataTypesDto.String,
            ItemTagDataTypesDto.TextArea,
            ItemTagDataTypesDto.Time,
            ItemTagDataTypesDto.Url,
            ItemTagDataTypesDto.Decimal,
            ItemTagDataTypesDto.Integer,
            ItemTagDataTypesDto.IntegerRange,
            ItemTagDataTypesDto.DecimalRange
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
        [JsonPropertyName("values"), JsonIgnoreEmptyCollection]        
        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();

    }
}
