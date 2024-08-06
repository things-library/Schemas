namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Attribute
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class ItemBasicAttributeSchema : SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Value
        /// </summary>
        [JsonPropertyName("value")]
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1)]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Value
        /// </summary>
        [JsonPropertyName("values")]
        [Display(Name = "Values"), StringLength(50, MinimumLength = 1)]
        public List<string> Values { get; set; } = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemBasicAttributeSchema()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemBasicAttributeSchema(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            this.Values = new() { value };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">Values</param>
        public ItemBasicAttributeSchema(string key, List<string> values)
        {
            this.Key = key;
            if (values.Any())
            {
                this.Value = values[0];
            }            
            this.Values = values;
        }
    }
}
