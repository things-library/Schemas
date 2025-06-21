// ================================================================================
// <copyright file="ItemTag.cs" company="Starlight Software Co">
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
    public class ItemTagDto : Base.SchemaBase
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
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1)]
        public virtual string Value { get; set; } = string.Empty;

        /// <summary>
        /// Data Type
        /// </summary>
        /// <remarks>This always return the first even if there are more than one.  Setting the value when more then one existing will remote the other items.</remarks>       
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
        public string DataType { get; set; } = TagDataTypes.String;

        /// <summary>
        /// Parent Item
        /// </summary>
        [JsonIgnore]
        public ItemDto? Parent { get; set; }

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
                this.DataType = TagDataTypes.Date;
            }
            else if (value is TimeOnly valueTime)
            {
                this.Value = valueTime.ToString("HH:mm:ss");
                this.DataType = TagDataTypes.Time;
            }
            else if (value is DateTime valueDateTime)
            {
                this.Value = valueDateTime.ToString("O");
                this.DataType = TagDataTypes.DateTime;
            }
            else if (value is DateTimeOffset valueDateTimeOffset)
            {
                this.Value = valueDateTimeOffset.ToString("O");
                this.DataType = TagDataTypes.Date;
            }
            else if (value is double valueDouble)
            {
                this.Value = $"{valueDouble}";
                this.DataType = TagDataTypes.Decimal;
            }
            else if (value is decimal valueDecimal)
            {
                this.Value = $"{valueDecimal}";
                this.DataType = TagDataTypes.Decimal;
            }
            else if (value is int valueInt)
            {
                this.Value = $"{valueInt}";
                this.DataType = TagDataTypes.Integer;
            }
            else if (value is Uri valueUrl)
            {
                this.Value = $"{valueUrl}";
                this.DataType = TagDataTypes.Url;
            }
            else if (value is bool valueBool)
            {
                this.Value = $"{valueBool}".ToLower();
                this.DataType = TagDataTypes.Boolean;
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
