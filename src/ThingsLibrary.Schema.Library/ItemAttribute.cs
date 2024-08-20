namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Attribute
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class ItemAttributeDto : Base.SchemaBase
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
        public virtual string Value
        {
            get => this.Values[0];
            set
            {
                // simple replace?
                if (this.Values.Count == 1) { this.Values[0] = value; }
                else
                {
                    // clear the listing and add one (this works for all != 1 cases)
                    this.Values.Clear();
                    this.Values.Add(value);
                }
            }
        }        

        /// <summary>
        /// Values
        /// </summary>
        /// <remarks>Collection should always have at least 1 cell</remarks>
        [JsonPropertyName("values")]
        [Display(Name = "Values"), StringLength(50, MinimumLength = 1)]
        public List<string> Values { get; set; } = new List<string>() { string.Empty };

        /// <summary>
        /// Data Type
        /// </summary>
        /// <remarks>This always return the first even if there are more than one.  Setting the value when more then one existing will remote the other items.</remarks>       
        [JsonPropertyName("type")]
        [Display(Name = "Data Type"), DefaultValue(AttributeDataTypes.String), Required]
        [AllowedValues(
            AttributeDataTypes.Boolean,
            AttributeDataTypes.Currency,
            AttributeDataTypes.CurrencyRange,
            AttributeDataTypes.Date,
            AttributeDataTypes.DateTime,
            AttributeDataTypes.Duration,
            AttributeDataTypes.Email,
            AttributeDataTypes.Enum,
            AttributeDataTypes.Html,
            AttributeDataTypes.Password,
            AttributeDataTypes.Phone,
            AttributeDataTypes.String,
            AttributeDataTypes.TextArea,
            AttributeDataTypes.Time,
            AttributeDataTypes.Url,
            AttributeDataTypes.Decimal,
            AttributeDataTypes.Integer,
            AttributeDataTypes.IntegerRange,
            AttributeDataTypes.DecimalRange
        )]
        public string DataType { get; set; } = AttributeDataTypes.String;

        /// <summary>
        /// Parent Item
        /// </summary>
        [JsonIgnore]
        public ItemDto? Parent { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemAttributeDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemAttributeDto(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemAttributeDto(string key, object value)
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
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">Values</param>
        public ItemAttributeDto(string key, List<string> values)
        {
            this.Key = key;
            this.Values = values;
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
                this.DataType = AttributeDataTypes.Date;
            }
            else if (value is TimeOnly valueTime)
            {
                this.Value = valueTime.ToString("HH:mm:ss");
                this.DataType = AttributeDataTypes.Time;
            }
            else if (value is DateTime valueDateTime)
            {
                this.Value = valueDateTime.ToString("O");
                this.DataType = AttributeDataTypes.DateTime;
            }
            else if (value is DateTimeOffset valueDateTimeOffset)
            {
                this.Value = valueDateTimeOffset.ToString("O");
                this.DataType = AttributeDataTypes.Date;
            }
            else if (value is decimal valueDecimal)
            {
                this.Value = $"{valueDecimal}";
                this.DataType = AttributeDataTypes.Decimal;
            }
            else if (value is int valueInt)
            {
                this.Value = $"{valueInt}";
                this.DataType = AttributeDataTypes.Integer;
            }
            else if (value is Uri valueUrl)
            {
                this.Value = $"{valueUrl}";
                this.DataType = AttributeDataTypes.Url;
            }
            else if (value is bool valueBool)
            {
                this.Value = $"{valueBool}".ToLower();
                this.DataType = AttributeDataTypes.Boolean;
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
        //        case AttributeDataTypes.String: { return this.Value; }
        //        case AttributeDataTypes.Date: { return DateOnly.ParseExact(this.Value, "yyyy-MM-dd"); }
        //        case AttributeDataTypes.Time: { return TimeOnly.ParseExact(this.Value, "HH:mm:ss"); }
        //        case AttributeDataTypes.DateTime: { return DateTime.Parse(this.Value); }

        //        default:
        //            {
        //                return this.Value;
        //            }
        //    }
        //}
    }
}
