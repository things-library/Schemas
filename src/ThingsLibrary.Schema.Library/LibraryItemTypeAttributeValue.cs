namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Attribute Value
    /// </summary>
    [DebuggerDisplay("{Name} ({Key})")]
    public class LibraryItemTypeAttributeValueDto : Base.SchemaBase
    {        
        /// <summary>
        /// Attribute Unique Key
        /// </summary>  
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;


        #region --- Extended ---

        public LibraryItemTypeAttributeDto? Attribute { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryItemTypeAttributeDto parent)
        {
            this.Attribute = parent;            
        }

        #endregion

        

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryItemTypeAttributeValueDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public LibraryItemTypeAttributeValueDto(string key, string name)
        {
            this.Key = key;
            this.Name = name;
        }
    }
}
