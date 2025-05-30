// ================================================================================
// <copyright file="Item.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Item Schema - Flexible
    /// </summary>
    [DebuggerDisplay("Key: {Key} (Name: {Name}, Type: {Type})")]
    public class ItemDto : Base.SchemaBase, IJsonOnDeserialized, IJsonOnSerializing
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
        [Display(Name = "Key"), StringLength(50)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date")]
        public DateTimeOffset? Date { get; set; }
                
        /// <summary>
        /// Item Type - describes what type of item we are talking about.
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]        
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Attributes
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("tags"), JsonConverter(typeof(ItemAttributesConverter)), JsonIgnoreEmptyCollection]
        [Display(Name = "Attribute Tags")]
        public ItemAttributesDto Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        [Display(Name = "Attachments")]
        public IDictionary<string, ItemDto> Items { get; set; } = new Dictionary<string, ItemDto>();


        #region --- Extended ---

        /// <summary>
        /// Parent Item Pointer
        /// </summary>
        [JsonIgnore]
        public ItemDto? Parent { get; set; }


        /// <summary>
        /// Root Item Pointer
        /// </summary>
        [JsonIgnore]
        public ItemDto Root { get; set; }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemDto()
        {
            this.Root = this;   //default until we know otherwise
        }

        /// <param name="type">Type</param>
        /// <param name="key">Unique Key</param>
        /// <param name="name">Name</param>
        /// <remarks>Name is automatically set to key value</remarks>
        public ItemDto(string type, string name, string key)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(type);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (!ItemDto.IsKeyValid(key)) { throw new ArgumentException("Invalid key."); }

            this.Root = this;

            this.Type = type;
            this.Name = name;
            this.Key = key;           
        }

        ///// <summary>
        ///// Constructor
        ///// </summary>        
        ///// <param name="type">Type</param>
        ///// <param name="name">Name</param>
        ///// <remarks>Name is automatically set to key value</remarks>
        //public ItemDto(string type, string name)
        //{
        //    ArgumentNullException.ThrowIfNullOrWhiteSpace(type);            
        //    ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

        //    this.Root = this;

        //    this.Type = type;
        //    this.Name = name;
        //    this.Key = name;            
        //}

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
            var attribute = new ItemAttributeDto(key, value);

            this.Add(attribute, append);
        }

        /// <summary>
        /// Add attribute to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="append">If value should be appended to existing</param>
        public void Add(string key, object value, bool append)
        {
            var attribute = new ItemAttributeDto(key, value);

            this.Add(attribute, append);
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(IEnumerable<ItemAttributeDto> attributes, bool append = false)
        {
            foreach (var attribute in attributes)
            {
                this.Add(attribute, append);
            }
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(IEnumerable<KeyValuePair<string, string>> attributes, bool append = false)
        {
            foreach (var attribute in attributes)
            {
                this.Add(attribute.Key, attribute.Value, append);
            }
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(IEnumerable<KeyValuePair<string, object>> attributes, bool append = false)
        {
            foreach (var attribute in attributes)
            {
                this.Add(attribute.Key, attribute.Value, append);
            }
        }

        /// <summary>
        /// Add attribute to the listing
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="append">If value(s) should be appended to existing</param>
        public void Add(ItemAttributeDto attribute, bool append = false)
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
        /// Gets a item based on the provided resource key 
        /// </summary>
        /// <param name="resourceKey">Resource Path</param>
        /// <returns></returns>
        /// <example>Key: child/grand_child/great_grand_child</example>
        public ItemDto? GetItem(string resourceKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceKey);

            ItemDto? item = this;

            var pathSegments = resourceKey.Split('/');
            foreach (var pathSegment in pathSegments)
            {
                if (string.IsNullOrWhiteSpace(pathSegment)) { return null; }

                // try to get the item, if failure then exit, otherwise we have it assignd to our item
                if (!item.Items.TryGetValue(pathSegment, out item))
                {
                    return null;
                }
            }

            return item;
        }

        /// <summary>
        /// Attach items to parent item
        /// </summary>
        /// <param name="childItems">Items to attach</param>
        public void Attach(IEnumerable<ItemDto> childItems)
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
        public void Attach(ItemDto childItem)
        {
            ArgumentNullException.ThrowIfNull(childItem);
            ArgumentException.ThrowIfNullOrWhiteSpace(childItem.Key);

            childItem.Parent = this;        // pretty obvious who the parent is
            childItem.Root = this.Root;     // well the child clearly isn't the root so our root must be its root
            
            this.Items.Add(childItem.Key, childItem);
        }

        /// <summary>
        /// Detatch 
        /// </summary>
        /// <param name="key">Key</param>
        public bool Detatch(string key) => this.Items.Remove(key);        

        /// <summary>
        /// Detatch All Attachments
        /// </summary>        
        public void DetatchAll() => this.Items.Clear();


        #endregion

        

        /// <summary>
        /// Establishes the child-parent linkage
        /// </summary>
        public void Init(ItemDto? parent)
        {
            // lets always have some kind of key
            if (string.IsNullOrEmpty(this.Key))
            {
                this.Key = this.Name.ToKey();
            }

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

            foreach(var item in this.Items)
            {
                // no idea how we got a item in the attachments dictionary that doesn't have a key.. lets fix it.
                if(item.Value.Key == null) { item.Value.Key = item.Key; }

                // recurse
                item.Value.Init(this);                
            }

            foreach (var attribute in this.Attributes)
            {                
                attribute.Parent = this;                
            }
        }

        #region --- Serialization / Deserialization ---

        /// <summary>
        /// Initialize the links
        /// </summary>
        public void OnDeserialized()
        {
            this.Init(null);

            // recurse
            foreach (var item in this.Items)
            {
                item.Value.OnDeserialized(item);
            }
        }

        /// <summary>
        /// Fix missing redundant stuff
        /// </summary>
        public void OnDeserialized(KeyValuePair<string, ItemDto> childItemPair)
        {
            childItemPair.Value.Key = childItemPair.Key;

            // recurse
            foreach (var item in this.Items)
            {
                item.Value.OnDeserialized(item);
            }
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

            // recurse
            foreach (var item in this.Items)
            {
                item.Value.OnDeserialized(item);
            }
        }

        public void OnSerializing(ItemDto childItem)
        {
            //because we are serilizing and children area already in a dictionary (so key is redundant to export)
            childItem.Key = string.Empty; 

            foreach (var item in this.Items.Values)
            {
                item.OnSerializing(item);
            }
        }

        #endregion

        /// <summary>
        /// Clone the current item
        /// </summary>
        /// <returns></returns>
        public ItemDto Clone()
        {
            // best way to detatch the hierarchy structure
            var clone = this.Clone<ItemDto>();

            // restore fields that aren't serialized
            clone.Id = this.Id;
            
            return clone;
        }
    }
}
