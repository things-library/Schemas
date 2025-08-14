// ================================================================================
// <copyright file="ItemTag.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Poc
{
    /// <summary>
    /// Tags
    /// </summary>
    [DebuggerDisplay("{Name}:{Value}, (Type: {Type}, Units: {Units})")]
    public class ItemTagDto
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        [Display(Name = "Value"), Required]
        public string Value { get; set; } = string.Empty;

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
        [JsonPropertyName("units"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Unit Symbol"), StringLength(20, MinimumLength = 1)]
        public string? Units { get; set; }

        /// <summary>
        /// Where in the list should this item show up priority wise?
        /// </summary>        
        [JsonPropertyName("seq"), JsonIgnoreDefault]
        [Display(Name = "Sequence Order"), Required]
        public short Sequence { get; set; } = 0;

        /// <summary>
        /// Tag Values
        /// </summary>
        /// <remarks>Only used for enum (picklist) tags</remarks>
        [JsonPropertyName("values"), JsonIgnoreEmptyCollection]
        public IDictionary<string, ItemTagValueDto> Values { get; set; } = new Dictionary<string, ItemTagValueDto>();

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTagDto()
        {
            //nothing
        }
    }
}
