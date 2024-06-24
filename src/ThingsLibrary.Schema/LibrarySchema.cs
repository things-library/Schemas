namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Library
    /// </summary>
    [DebuggerDisplay("{Name} ({Key})")]
    public class LibrarySchema : Base.SchemaBase
    {        
        /// <summary>
        /// Unique Key
        /// </summary> 
        /// <remarks>This is used to align records to know if it is new or update to a existing library</remarks>
        [JsonPropertyName("key"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Key, Display(Name = "Key"), StringLength(50, MinimumLength = 5)]
        [RegularExpression(Base.SchemaBase.RootKeyPattern, ErrorMessage = Base.SchemaBase.RootKeyPatternDescription )]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Name
        /// </summary>
        [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Item Types - what kind of items do we expect and how do we explain them
        /// </summary>
        [ValidateObject<ItemTypeSchema>]
        [JsonPropertyName("types"), JsonIgnoreEmptyCollection]
        public List<ItemTypeSchema> ItemTypes { get; set; } = new();

        /// <summary>
        /// Items
        /// </summary>
        /// <remarks>This is required as a empty isn't a library of anything and this helps verify schema types if $schema is missing</remarks>
        [ValidateObject<ItemSchema>, Required]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        public List<ItemSchema> Items { get; set; } = new();


        /// <summary>
        /// Constructor
        /// </summary>
        public LibrarySchema()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="name">Name</param>
        public LibrarySchema(string key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}
