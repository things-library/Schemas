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
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Attributes
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("attributes"), JsonIgnoreEmptyCollection]
        public Dictionary<string, ItemTypeAttributeSchema> Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("attachments"), JsonIgnoreEmptyCollection]
        public Dictionary<string, ItemTypeAttachmentSchema> Attachments { get; set; } = new();


        #region --- Extended ---

        public LibrarySchema? Library { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibrarySchema parent)
        {
            this.Library = parent;

            // fix all of the reference variables
            foreach (var pair in this.Attributes)
            {
                pair.Value.Key = pair.Key;                
                pair.Value.Init(this);
            }

            foreach (var pair in this.Attachments)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);                
            }
        }

        #endregion

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

