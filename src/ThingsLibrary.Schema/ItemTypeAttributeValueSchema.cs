namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Attribute Value
    /// </summary>
    [DebuggerDisplay("{Name} ({Key})")]
    public class ItemTypeAttributeValueSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Attribute Unique Key
        /// </summary>  
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 2), Required]
        [RegularExpression("^[a-z0-9_]*$", ErrorMessage = "Invalid Characters.  Please only use lowercase letters, numeric, and underscores.")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTypeAttributeValueSchema()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public ItemTypeAttributeValueSchema(string key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}
