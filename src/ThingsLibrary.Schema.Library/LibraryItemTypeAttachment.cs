namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item type attachment
    /// </summary>    
    [DebuggerDisplay("{Type} ({Name})")]
    public class LibraryItemTypeAttachment : Base.SchemaBase
    {
        /// <summary>
        /// Attachment Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Item Type
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name (Override)
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        #region --- Extended ---

        public LibraryItemTypeDto? ItemType { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryItemTypeDto parent)
        {
            // fix all of the reference variables
            this.ItemType = parent;
        }

        #endregion

        /// <summary>
        /// Item Type Attachment
        /// </summary>
        public LibraryItemTypeAttachment()
        {
            //nothing
        }
    }
}

