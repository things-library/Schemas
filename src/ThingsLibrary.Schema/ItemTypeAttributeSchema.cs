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
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Data Type that this attribute represents.. like pick list, date-time, etc
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Data Type"), DefaultValue("string"), Required]
        [AllowedValues(
            "boolean",
            "currency",
            "currency_range",
            "date",
            "date_time",
            "decimal",
            "decimal_range",
            "duration",
            "email",
            "enum",            
            "html",
            "int",
            "int_range",
            "password",
            "phone",
            "string",
            "text",
            "time",
            "url"
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
        /// Attribute Values
        /// </summary>
        /// <remarks>Only used for enum (picklist) attributes</remarks>
        [JsonPropertyName("values"), JsonConverter(typeof(ItemTypeAttributeValueConverter)), JsonIgnoreEmptyCollection]
        [ValidateCollectionItems]
        public Dictionary<string, ItemTypeAttributeValueSchema> Values { get; set; } = new();

        #region --- Extended ---

        public ItemTypeSchema? ItemType { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(ItemTypeSchema parent)
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
