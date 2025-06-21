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
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string? Key { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? Date { get; set; }
                
        /// <summary>
        /// Item Type - describes what type of item we are talking about.
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]        
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("tags"), JsonConverter(typeof(ItemTagsConverter)), JsonIgnoreEmptyCollection]
        [Display(Name = "Tags")]
        public ItemTagsDto Tags { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        [Display(Name = "Attachment Items")]
        public IDictionary<string, ItemDto> Items { get; set; } = new Dictionary<string, ItemDto>();


        #region --- Extended ---

        /// <summary>
        /// Root Item Pointer
        /// </summary>
        [JsonIgnore]
        public ItemDto Root { get; set; }

        /// <summary>
        /// Parent Item Pointer
        /// </summary>
        [JsonIgnore]
        public ItemDto? Parent { get; set; }
                
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

        #region --- Tags ---

        /// <summary>
        /// Get and set tag values
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => this.Tags[key];
            set => this.Tags[key] = value;            
        }

        /// <summary>
        /// Get tag value or default
        /// </summary>
        /// <typeparam name="T">Data Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Tag value or default value</returns>
        public T Get<T>(string key, T defaultValue) => this.Tags.Get<T>(key, defaultValue);

        /// <summary>
        /// Get tag value or default
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Tag value or default value</returns>
        public string Get(string key, string defaultValue) => this.Tags.Get<string>(key, defaultValue);

        /// <summary>
        /// Add tag to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(string key, string value)
        {
            var tag = new ItemTagDto(key, value);

            this.Add(tag);
        }

        /// <summary>
        /// Add tag to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(string key, object value)
        {
            var tag = new ItemTagDto(key, value);

            this.Add(tag);
        }

        /// <summary>
        /// Add basic collection of tags to the listing
        /// </summary>
        /// <param name="tags">Flat listing of Item Basic Tags</param>
        public void Add(IEnumerable<ItemTagDto> tags)
        {
            foreach (var tag in tags)
            {
                this.Add(tag);
            }
        }

        /// <summary>
        /// Add basic collection of tags to the listing
        /// </summary>
        /// <param name="tags">Flat listing of Item Basic tags</param>
        public void Add(IEnumerable<KeyValuePair<string, string>> tags)
        {
            foreach (var tag in tags)
            {
                this.Add(tag.Key, tag.Value);
            }
        }

        /// <summary>
        /// Add basic collection of tags to the listing
        /// </summary>
        /// <param name="tags">Flat listing of Item Basic tags</param>
        public void Add(IEnumerable<KeyValuePair<string, object>> tags)
        {
            foreach (var tag in tags)
            {
                this.Add(tag.Key, tag.Value);
            }
        }

        /// <summary>
        /// Add tag to the listing
        /// </summary>
        /// <param name="tag">tag</param>
        public void Add(ItemTagDto tag)
        {
            tag.Parent = this;    //clearly we are the parent

            this.Tags.Add(tag);
        }

        /// <summary>
        /// Remove tag(s) with the key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if item was found in collection, Fals if not found</returns>
        public bool Remove(string key) => this.Tags.Remove(key);

        /// <summary>
        /// Remove all tags
        /// </summary>
        public void RemoveAll() => this.Tags.Clear();

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
            //// lets always have some kind of key
            //if (string.IsNullOrEmpty(this.Key))
            //{
            //    this.Key = this.Name.ToKey();
            //}

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

            foreach (var tag in this.Tags)
            {                
                tag.Parent = this;                
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
