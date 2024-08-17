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
        public string Value
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
        public List<string> Values { get; set; } = new() { string.Empty };  // array should always have at least one cell


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
        /// <param name="values">Values</param>
        public ItemAttributeDto(string key, List<string> values)
        {
            this.Key = key;            
            this.Values = values;
        }
    }
}
