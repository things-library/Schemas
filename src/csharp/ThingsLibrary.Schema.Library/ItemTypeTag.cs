// ================================================================================
// <copyright file="ItemTypeTag.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.ComponentModel.DataAnnotations.Schema;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Tags
    /// </summary>
    [DebuggerDisplay("{Name}, (Type: {Type}, Units: {Units})")]
    public class ItemTypeTagDto
    {        
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
            ItemTagDataTypesDto.Enum,   //pick-list
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
        /// Position (1 = top)
        /// </summary>        
        [JsonPropertyName("seq"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Display Order (Sequence)")]
        public short? Sequence { get; set; }

        /// <summary>
        /// Tag Values
        /// </summary>
        /// <remarks>Only used for enum (pick-list) tags</remarks>
        [JsonPropertyName("values"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTypeTagDto()
        {
            //nothing
        }

        /// <summary>
        /// Easy lookup
        /// </summary>
        /// <param name="key">Tag Key</param>
        /// <param name="isMeta">If value is in metadata</param>
        /// <returns></returns>
        public string this[string key, bool isMeta]
        {
            get
            {
                if (isMeta)
                {
                    if (!this.Meta.ContainsKey(key)) { return string.Empty; }

                    return this.Meta[key];
                }
                else
                {
                    if (!this.Values.ContainsKey(key)) { return string.Empty; }

                    return this.Values[key];
                }
                
            }
        }
    }
}
