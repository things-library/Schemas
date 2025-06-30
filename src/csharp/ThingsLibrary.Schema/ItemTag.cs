// ================================================================================
// <copyright file="ItemTag.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Tag
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class ItemTagDto
    {
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Value
        /// </summary>
        /// <remarks>This always return the first even if there are more than one.  Setting the value when more then one existing will remote the other items.</remarks>
        [JsonPropertyName("value")]
        [Display(Name = "Value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Data Type
        /// </summary>
        /// <remarks>This always return the first even if there are more than one.  Setting the value when more then one existing will remote the other items.</remarks>       
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
        public string DataType { get; set; } = ItemTagDataTypesDto.String;

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTagDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemTagDto(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemTagDto(string key, object value)
        {
            this.Key = key;
            if (value is string valueStr)
            {
                this.Value = valueStr;
            }
            else
            {
                this.SetValue(value);
            }            
        }
                
        /// <summary>
        /// Set value / data type based on object value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            if (value is string valueStr)
            {
                this.Value = valueStr;     //no need to set the data type as it is defaulted to 'string'                       
            }
            else if (value is DateOnly valueDate)
            {
                this.Value = valueDate.ToString("yyyy-MM-dd");
                this.DataType = ItemTagDataTypesDto.Date;
            }
            else if (value is TimeOnly valueTime)
            {
                this.Value = valueTime.ToString("HH:mm:ss");
                this.DataType = ItemTagDataTypesDto.Time;
            }
            else if (value is DateTime valueDateTime)
            {
                this.Value = valueDateTime.ToString("O");
                this.DataType = ItemTagDataTypesDto.DateTime;
            }
            else if (value is DateTimeOffset valueDateTimeOffset)
            {
                this.Value = valueDateTimeOffset.ToString("O");
                this.DataType = ItemTagDataTypesDto.Date;
            }
            else if (value is double valueDouble)
            {
                this.Value = $"{valueDouble}";
                this.DataType = ItemTagDataTypesDto.Decimal;
            }
            else if (value is decimal valueDecimal)
            {
                this.Value = $"{valueDecimal}";
                this.DataType = ItemTagDataTypesDto.Decimal;
            }
            else if (value is int valueInt)
            {
                this.Value = $"{valueInt}";
                this.DataType = ItemTagDataTypesDto.Integer;
            }
            else if (value is Uri valueUrl)
            {
                this.Value = $"{valueUrl}";
                this.DataType = ItemTagDataTypesDto.Url;
            }
            else if (value is bool valueBool)
            {
                this.Value = $"{valueBool}".ToLower();
                this.DataType = ItemTagDataTypesDto.Boolean;
            }
            else
            {
                //DEFAULT VALUE / DATA TYPE
                this.Value = $"{value}";     //no need to set the data type as it is defaulted to 'string'            
            }
        }

        //public object GetValue()
        //{
        //    switch (this.DataType.Key)
        //    {
        //        case TagDataTypes.String: { return this.Value; }
        //        case TagDataTypes.Date: { return DateOnly.ParseExact(this.Value, "yyyy-MM-dd"); }
        //        case TagDataTypes.Time: { return TimeOnly.ParseExact(this.Value, "HH:mm:ss"); }
        //        case TagDataTypes.DateTime: { return DateTime.Parse(this.Value); }

        //        default:
        //            {
        //                return this.Value;
        //            }
        //    }
        //}
    }
}
