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
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
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

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryDto(string key, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            if (!SchemaBase.IsKeyValid(key)) { throw new ArgumentException(SchemaBase.KeyPatternErrorMessage); }

            this.Key = key;
            this.Name = name;
        }

        public LibraryItemDto? this[string key]
        {
            get => (this.Items.ContainsKey(key) ? this.Items[key] : null);

            set
            {
                // if setting the key to null.. then remove it (if exists)
                if (value == null)
                {
                    this.Items.Remove(key);
                }
                else
                {
                    this.Items[key] = value;
                }                
            }

        }

        #region --- Add Item ---

        public void Add(IEnumerable<ItemDto> basicItems)
        {
            foreach (var basicItem in basicItems)
            {
                this.Add(basicItem);
            }
        }

        public void Add(ItemDto basicItem)
        {
            //convert basic item to valid library item

            // if the key is missing then take it from the required name field
            if (string.IsNullOrWhiteSpace(basicItem.Key)) { basicItem.Key = basicItem.Name; }

            if (!SchemaBase.IsKeyValid(basicItem.Key))
            {
                basicItem.Key = basicItem.Key.ToKey();
            }

            if (string.IsNullOrWhiteSpace(basicItem.Name))
            {
                //convert key to some sort of display name
            }

            throw new NotImplementedException();
        }


        public void Add(IEnumerable<LibraryItemDto> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public void Add(LibraryItemDto item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
