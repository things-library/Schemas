namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item Schema - Flexible
    /// </summary>
    [DebuggerDisplay("Key: {Key} (Name: {Name}, Type: {Type})")]
    public class BasicItemDto : Base.SchemaBase, IJsonOnDeserialized, IJsonOnSerializing
    {        
        //// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri? SchemaUrl { get; set; }

        /// <summary>
        /// Resource Key
        /// </summary>  
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
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

        /// <param name="type">Type</param>
        /// <param name="key">Unique Key</param>
        /// <param name="name">Name</param>
        /// <remarks>Name is automatically set to key value</remarks>
        public BasicItemDto(string type, string name, string key)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(type);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            this.Root = this;

            this.Type = type;
            this.Name = name;
            this.Key = key;           
        }

        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="type">Type</param>
        /// <param name="name">Name</param>
        /// <remarks>Name is automatically set to key value</remarks>
        public BasicItemDto(string type, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(type);            
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            this.Root = this;

            this.Type = type;
            this.Name = name;
            this.Key = name;            
        }

        #region --- Attributes ---

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
        /// Add attribute to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="append">If value should be appended to existing</param>
        public void Add(string key, string value, bool append)
        {
            var attribute = new BasicItemAttributeDto(key, value);
            
            this.Add(attribute, append);
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(IEnumerable<BasicItemAttributeDto> attributes, bool append = false)
        {            
            foreach (var attribute in attributes)
            {                
                this.Add(attributes, append);
            }
        }

        /// <summary>
        /// Add attribute to the listing
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(BasicItemAttributeDto attribute, bool append = false)
        {
            attribute.Parent = this;    //clearly we are the parent

            this.Attributes.Add(attribute, append);
        }

        /// <summary>
        /// Remove attribute(s) with the key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if item was found in collection, Fals if not found</returns>
        public bool Remove(string key) => this.Attributes.Remove(key);

        /// <summary>
        /// Remove all attributes
        /// </summary>
        public void RemoveAll() => this.Attributes.Clear();

        #endregion

        #region --- Attachments ---

        /// <summary>
        /// Attach items to parent item
        /// </summary>
        /// <param name="childItems">Items to attach</param>
        public void Attach(IEnumerable<BasicItemDto> childItems)
        {
            foreach(var childItem in childItems)
            {
                this.Attach(childItem);
            }
        }

        /// <summary>
        /// Attach item to parent item
        /// </summary>
        /// <param name="childItem">Items to attach</param>
        public void Attach(BasicItemDto childItem)
        {
            childItem.Parent = this;        // pretty obvious who the parent is
            childItem.Root = this.Root;     // well the child clearly isn't the root so our root must be its root

            this.Attachments.Add(childItem);
        }

        /// <summary>
        /// Detatch 
        /// </summary>
        /// <param name="key">Key</param>
        public bool Detatch(string key)
        {
            var childItem = this.Attachments.FirstOrDefault(x => x.Key == key);
            if(childItem == null) { return false; }

            return this.Attachments.Remove(childItem);
        }

        /// <summary>
        /// Detatch All Attachments
        /// </summary>        
        public void DetatchAll() => this.Attachments.Clear();
        

        #endregion

        /// <summary>
        /// Establishes the child-parent linkage
        /// </summary>
        public void Init(BasicItemDto? parent)
        {
            // must be at the top of the tree
            if(parent == null)
            {
                this.Root = this;   // well we are the root if we have no parents
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

        /// <summary>
        /// Initialize the links
        /// </summary>
        public void OnSerializing()
        {
            // attempt to output the schema URL for root serializing
            if(this.Root == this)
            {
                this.SchemaUrl = new Uri($"{Base.SchemaBase.SchemaBaseUrl}/item.json");
            }            
        }

        /// <summary>
        /// Clone the current item
        /// </summary>
        /// <returns></returns>
        public BasicItemDto Clone()
        {
            // best way to detatch the hierarchy structure
            var clone = this.Clone<BasicItemDto>();

            // restore fields that aren't serialized
            clone.Id = this.Id;
            
            return clone;
        }
    }
}
