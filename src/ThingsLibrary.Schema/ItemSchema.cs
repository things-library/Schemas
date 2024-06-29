namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Library Item
    /// </summary>
    [DebuggerDisplay("{Name} (Key: {Key}, Type: {Type})")]
    public class ItemSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Resource Key
        /// </summary>  
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
                
        /// <summary>
        /// Item Type 
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Attributes
        /// </summary>         
        [JsonPropertyName("attributes"), JsonConverter(typeof(ItemAttributeConverter)), JsonIgnoreEmptyCollection]
        public Dictionary<string, ItemAttributeSchema> Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("attachments"), JsonIgnoreEmptyCollection]
        public Dictionary<string, ItemSchema> Attachments { get; set; } = new();

        #region --- Extended ---

        /// <summary>
        /// Library
        /// </summary>
        [JsonIgnore]
        public LibrarySchema? Library { get; set; }

        /// <summary>
        /// Item Type
        /// </summary>
        [JsonIgnore]
        public ItemTypeSchema? ItemType { get; set; }

        /// <summary>
        /// Root Library Item
        /// </summary>
        [JsonIgnore]
        public ItemSchema? RootItem { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibrarySchema parent)
        {
            this.Library = parent;
            if (parent.ItemTypes.ContainsKey(this.Type)) { this.ItemType = parent.ItemTypes[this.Type]; }
            if (this.RootItem == null) { this.RootItem = this; }

            // fix all of the reference variables
            foreach(var pair in this.Attributes)
            {
                pair.Value.Key = pair.Key;                
                pair.Value.Init(this);
            }

            foreach (var pair in this.Attachments)
            {
                pair.Value.Key = pair.Key;
                pair.Value.RootItem = this.RootItem;
                pair.Value.Init(parent);
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemSchema()
        {
            //nothing
        }
    }
}
