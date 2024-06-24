namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Item types
    /// </summary>    
    [DebuggerDisplay("{Name} ({Key})")]
    public class ItemTypeSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>        
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(Base.SchemaBase.RootKeyPattern, ErrorMessage = Base.SchemaBase.RootKeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("description")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indicates that this item type is managed by the system and can't be edited by a user
        /// </summary>
        [JsonPropertyName("system")]
        [Display(Name = "System")]
        public bool? IsSystem { get; set; }

        /// <summary>
        /// Limit attachments to only ones listed.
        /// </summary>
        [JsonPropertyName("locked")]
        [Display(Name = "Locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Attributes
        /// </summary>
        [ValidateObject<ItemTypeAttributeSchema>]
        [JsonPropertyName("attributes"), JsonIgnoreEmptyCollection]
        public List<ItemTypeAttributeSchema> Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateObject<ItemTypeAttachmentSchema>]
        [JsonPropertyName("attachments"), JsonIgnoreEmptyCollection]
        public List<ItemTypeAttachmentSchema> Attachments { get; set; } = new();

        
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTypeSchema()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public ItemTypeSchema(string key, string name)
        {
            this.Key = key;
            this.Name = name;
        }
    }
}

