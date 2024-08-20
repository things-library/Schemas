namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Attribute
    /// </summary>    
    public interface IItemAttribute
    {
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>        
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        public string Key { get; }

        /// <summary>
        /// Value
        /// </summary>
        /// <remarks>This always return the first even if there are more than one.  Setting the value when more then one existing will remote the other items.</remarks>
        [JsonPropertyName("value")]
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1)]
        public string Value { get; }
        
        /// <summary>
        /// Keep track of the item data type
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(AttributeDataTypeConverter))]
        public AttributeDataTypeDto DataType { get; }

        /// <summary>
        /// Parent Item
        /// </summary>
        [JsonIgnore]
        public ItemDto? Parent { get; set; }
    }
}
