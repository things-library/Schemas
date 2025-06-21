// ================================================================================
// <copyright file="LibraryItem.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Library Item
    /// </summary>
    [DebuggerDisplay("{Name} (Key: {Key}, Type: {Type})")]
    public class LibraryItemDto : Base.SchemaBase
    {        
        /// <summary>
        /// Resource Key
        /// </summary>  
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(SchemaBase.KeyPattern, ErrorMessage = SchemaBase.KeyPatternErrorMessage)]
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
        public DateTimeOffset? Date { get; set; }
                
        /// <summary>
        /// Item Type 
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>         
        [JsonPropertyName("tags"), JsonConverter(typeof(LibraryItemTagsConverter)), JsonIgnoreEmptyCollection]
        public LibraryItemTagsDto Tags { get; set; } = new LibraryItemTagsDto();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("items"), JsonIgnoreEmptyCollection]
        public IDictionary<string, LibraryItemDto> Items { get; set; } = new Dictionary<string, LibraryItemDto>();

        #region --- Extended ---

        /// <summary>
        /// Library
        /// </summary>
        [JsonIgnore]
        public LibraryDto? Library { get; set; }

        /// <summary>
        /// Item Type
        /// </summary>
        [JsonIgnore]
        public LibraryItemTypeDto? ItemType { get; set; }

        /// <summary>
        /// Root Library Item
        /// </summary>
        [JsonIgnore]
        public LibraryItemDto? Root { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        [JsonIgnore]
        public LibraryItemDto? Parent { get; set; }

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between things and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(LibraryDto library, LibraryItemDto? parent)
        {
            this.Library = library;
            this.Parent = parent;

            // if parent then the root is the parent's root
            if (parent != null) { this.Root = parent.Root; }
            
            if (!library.Types.ContainsKey(this.Type)) { throw new ArgumentException($"Missing library item type '{this.Type}'."); }
            this.ItemType = library.Types[this.Type]; 
            
            // fix all of the reference variables
            foreach(var pair in this.Tags)
            {                           
                pair.Init(this);
            }

            // children
            foreach (var pair in this.Items)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(library, this);
            }
        }

        #endregion

        #region --- Serialization / Deserialization ---

        ///// <summary>
        ///// Initialize the links
        ///// </summary>
        //public void OnDeserialized()
        //{
        //    this.Init(null);

        //    // recurse
        //    foreach (var item in this.Items)
        //    {
        //        item.Value.OnDeserialized(item);
        //    }
        //}

        public void OnSerializing(LibraryItemDto childItem)
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
        /// Constructor
        /// </summary>
        public LibraryItemDto()
        {
            this.Root = this;   //default until we know otherwise
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryItemDto(string type, string key, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(type);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);            
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            if (!SchemaBase.IsKeyValid(type)) { throw new ArgumentException(SchemaBase.KeyPatternErrorMessage); }
            if (!SchemaBase.IsKeyValid(key)) { throw new ArgumentException(SchemaBase.KeyPatternErrorMessage); }

            this.Type = type;
            this.Key = key;
            this.Name = name;            
        }

        /// <summary>
        /// Adds the flat listing of tags (replacing existing)
        /// </summary>
        /// <param name="tags">Flat tag listing</param>
        public void Add(IEnumerable<LibraryItemTagDto> tags)
        {
            ArgumentNullException.ThrowIfNull(tags);

            foreach (var tag in tags)
            {
                this.Tags[tag.Key] = tag.Value;  //TODO:
            }
        }


        /// <summary>
        /// Gets a item based on the provided resource key 
        /// </summary>
        /// <param name="resourceKey">Resource Path</param>
        /// <returns></returns>
        /// <example>Key: child/grand_child/great_grand_child</example>
        public LibraryItemDto? GetItem(string resourceKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceKey);

            var item = this;

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
    }
}
