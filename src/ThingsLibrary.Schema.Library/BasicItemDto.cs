namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item Schema - Flexible
    /// </summary>
    [DebuggerDisplay("Key: {Key} (Name: {Name}, Type: {Type})")]
    public class BasicItemDto : Base.SchemaBase, IJsonOnDeserialized
    {        
        /// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri? SchemaUrl { get; set; } = new Uri($"{Base.SchemaBase.SchemaBaseUrl}/item.json");

        /// <summary>
        /// Resource Key
        /// </summary>  
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string? Key { get; set; }

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
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Attributes
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("attributes"), JsonConverter(typeof(BasicItemAttributesConverter)), JsonIgnoreEmptyCollection]
        public BasicItemAttributesDto Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("attachments"), JsonIgnoreEmptyCollection]
        public List<BasicItemDto> Attachments { get; set; } = new();


        #region --- Extended ---

        /// <summary>
        /// Parent Item Pointer
        /// </summary>
        [JsonIgnore]
        public BasicItemDto? Parent { get; set; }


        /// <summary>
        /// Root Item Pointer
        /// </summary>
        [JsonIgnore]
        public BasicItemDto Root { get; set; }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public BasicItemDto()
        {
            this.Root = this;   //default until we know otherwise
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Unique Key</param>
        /// <param name="type">Item Type</param>
        /// <remarks>Name is automatically set to key value</remarks>
        public BasicItemDto(string key, string type)
        {
            this.Type = type;

            this.Key = key;
            this.Name = key;

            this.Root = this;
        }

        /// <summary>
        /// Get and set attribute values
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => this.Attributes[key];
            set => this.Attributes[key] = value;            
        }

        /// <summary>
        /// Get attribute value or default
        /// </summary>
        /// <typeparam name="T">Data Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Attribute value or default value</returns>
        public T Get<T>(string key, T defaultValue) => this.Attributes.Get<T>(key, defaultValue);

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        public void Add(IEnumerable<BasicItemAttributeDto> attributes, bool append = false) => this.Attributes.Add(attributes, append);

        /// <summary>
        /// Add attribute to the listing
        /// </summary>
        /// <param name="attribute">Attribute</param>
        public void Add(BasicItemAttributeDto attribute, bool append = false) => this.Attributes.Add(attribute, append);

        /// <summary>
        /// Add attribute to listing
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="append"></param>
        public void Add(string key, string value, bool append) => this.Attributes.Add(key, value, append);

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        public void Add(IEnumerable<BasicItemDto> childItems) => this.Attachments.AddRange(childItems);

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        public void Add(BasicItemDto childItem) => this.Attachments.Add(childItem);

        /// <summary>
        /// Establishes the child-parent linkage
        /// </summary>
        public void Init(BasicItemDto? parent)
        {
            // must be at the top of the tree
            if(parent == null)
            {
                this.Root = this;
            }
            else
            { 
                this.Parent = parent;
                this.Root = parent.Root;
            }

            foreach(var item in this.Attachments)
            {
                // recurse
                item.Init(this);                
            }

            foreach (var attribute in this.Attributes)
            {
                attribute.Parent = this;
            }
        }

        /// <summary>
        /// Initialize the links
        /// </summary>
        public void OnDeserialized()
        {
            this.Init(null);
        }
    }
}
