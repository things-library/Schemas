using ThingsLibrary.Schema;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Library
    /// </summary>
    [DebuggerDisplay("{Name} ({Key})")]
    public class LibraryDto : Base.SchemaBase, IJsonOnDeserialized
    {
        /// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri SchemaUrl { get; set; } = new Uri($"{Base.SchemaBase.SchemaBaseUrl}/library.json");
                
        /// <summary>
        /// Unique Key
        /// </summary> 
        /// <remarks>This is used to align records to know if it is new or update to a existing library</remarks>
        [JsonPropertyName("key"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Key, Display(Name = "Key"), StringLength(50, MinimumLength = 1)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string? Key { get; set; }

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 5)]
        public string? Name { get; set; }

        /// <summary>
        /// Item Types - what kind of items do we expect and how do we explain them
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("types"), Required]
        public Dictionary<string, LibraryItemTypeDto> ItemTypes { get; set; } = new();

        /// <summary>
        /// Items
        /// </summary>
        /// <remarks>This is required as a empty isn't a library of anything and this helps verify schema types if $schema is missing</remarks>
        [ValidateCollectionItems, Required]
        [JsonPropertyName("items")]
        public Dictionary<string, LibraryItemDto> Items { get; set; } = new();

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init()
        {
            // fix all of the reference variables
            foreach(var pair in this.ItemTypes)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);                
            } 
            
            foreach(var pair in this.Items)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);
            }
        }

        public void OnDeserialized()
        {
            this.Init();
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryDto()
        {
            //nothing
        }        
    }
}
