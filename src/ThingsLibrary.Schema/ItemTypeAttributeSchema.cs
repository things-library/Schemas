namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Attribute
    /// </summary>
    [DebuggerDisplay("{Name}, ({Key}, Type: {Type}, Units: {Units})")]
    public class ItemTypeAttributeSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Data Type that this attribute represents.. like pick list, date-time, etc
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Data Type"), DefaultValue(AttributeDataTypes.String)]
        [AllowedValues(
            AttributeDataTypes.Boolean,
            AttributeDataTypes.Currency,
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
            AttributeDataTypes.Value,
            AttributeDataTypes.ValueInt,
            AttributeDataTypes.ValueRange,
            AttributeDataTypes.ValueIntRange,
            AttributeDataTypes.CurrencyRange
        )]
        public string Type { get; set; } = AttributeDataTypes.String;

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
        /// Is this field required to be entered
        /// </summary>
        [JsonPropertyName("required")]
        [Display(Name = "Required"), Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Indicates that this attribute is managed by the system and can't be edited by a user
        /// </summary>
        [JsonPropertyName("system")]
        [Display(Name = "System")]
        public bool? IsSystem { get; set; }

        /// <summary>
        /// Allow multiple of this attribute attached to a Thing
        /// </summary>
        /// <example>"Genre": ["Comedy","Romance"]</example>
        /// <remarks>Only used for enum (picklist) attributes</remarks>
        [JsonPropertyName("multi")]
        [Display(Name = "Allow Multiples"), DefaultValue(false), Required]
        public bool IsAllowMultiples { get; set; }

        /// <summary>
        /// Prevent new attribute values from being added
        /// </summary>
        /// <remarks>Only used for enum (picklist) attributes</remarks>
        [JsonPropertyName("locked")]
        [Display(Name = "Locked"), Required]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Attribute Values
        /// </summary>
        /// <remarks>Only used for enum (picklist) attributes</remarks>
        [JsonPropertyName("values"), JsonConverter(typeof(AttributeValueConverter)), JsonIgnoreEmptyCollection]
        [ValidateObject<ItemTypeAttributeValueSchema>]
        public Dictionary<string, ItemTypeAttributeValueSchema> Values { get; set; } = new();
    }   
}
