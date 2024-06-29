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


        #region --- Extended ---

        public ItemTypeAttributeSchema? Attribute { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(ItemTypeAttributeSchema parent)
        {
            this.Attribute = parent;            
        }

        #endregion

        

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
