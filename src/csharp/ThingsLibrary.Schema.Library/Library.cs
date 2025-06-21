// ================================================================================
// <copyright file="Library.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Library
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class LibraryDto : Base.SchemaBase, IJsonOnDeserialized
    {
        /// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri SchemaUrl { get; set; } = new Uri($"{Base.SchemaBase.SchemaBaseUrl}/library.json");

        /// <summary>
        /// Tag Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 5)]
        public string? Name { get; set; }

        /// <summary>
        /// Item Types - what kind of items do we expect and how do we explain them
        /// </summary>
        /// <remarks>This is required as empty isn't a library of anything and this helps verify schema types if $schema is missing</remarks>
        [ValidateCollectionItems]
        [JsonPropertyName("types"), Required]
        public IDictionary<string, LibraryItemTypeDto> Types { get; set; } = new Dictionary<string, LibraryItemTypeDto>();

        /// <summary>
        /// Items
        /// </summary>
        /// <remarks>This is required as a empty isn't a library of anything and this helps verify schema types if $schema is missing</remarks>
        [ValidateCollectionItems, Required]
        [JsonPropertyName("items")]
        public IDictionary<string, LibraryItemDto> Items { get; set; } = new Dictionary<string, LibraryItemDto>();

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching tags and item types.  Creates the relationships between things and tags
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init()
        {
            // fix all of the reference variables
            foreach(var pair in this.Types)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this);                
            } 
            
            foreach(var pair in this.Items)
            {
                pair.Value.Key = pair.Key;
                pair.Value.Init(this, null);
            }
        }

        #endregion

        #region --- Serialization / Deserialization ---

        /// <summary>
        /// Initialize the links
        /// </summary>
        public void OnDeserialized()
        {
            this.Init();
        }

        /// <summary>
        /// Initialize the links
        /// </summary>
        public void OnSerializing()
        {
            //// recurse
            //foreach (var item in this.Items)
            //{
            //    item.Value.OnSerializing(item);
            //}
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
        public LibraryDto(string name)
        {            
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            
            this.Name = name;
        }

        //public LibraryItemDto? this[string key]
        //{
        //    get => (this.Items.ContainsKey(key) ? this.Items[key] : null);

        //    set
        //    {
        //        // if setting the key to null.. then remove it (if exists)
        //        if (value == null)
        //        {
        //            this.Items.Remove(key);
        //        }
        //        else
        //        {
        //            this.Items[key] = value;
        //        }                
        //    }

        //}

        //#region --- Add Item ---

        //public void Add(IEnumerable<ItemDto> basicItems)
        //{
        //    foreach (var basicItem in basicItems)
        //    {
        //        this.Add(basicItem);
        //    }
        //}

        //public void Add(ItemDto basicItem)
        //{
        //    //convert basic item to valid library item

        //    // if the key is missing then take it from the required name field
        //    if (string.IsNullOrWhiteSpace(basicItem.Key)) { basicItem.Key = basicItem.Name; }

        //    if (!SchemaBase.IsKeyValid(basicItem.Key))
        //    {
        //        basicItem.Key = basicItem.Key.ToKey();
        //    }

        //    if (string.IsNullOrWhiteSpace(basicItem.Name))
        //    {
        //        //convert key to some sort of display name
        //    }

        //    throw new NotImplementedException();
        //}


        //public void Add(IEnumerable<LibraryItemDto> items)
        //{
        //    foreach (var item in items)
        //    {
        //        this.Add(item);
        //    }
        //}

        //public void Add(LibraryItemDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        /// <summary>
        /// Gets a item based on the provided resource key 
        /// </summary>
        /// <param name="resourceKey">Resource Path</param>
        /// <returns></returns>
        /// <example>Key: child/grand_child/great_grand_child</example>
        //public LibraryItemDto? GetItem(string resourceKey)
        //{
        //    ArgumentNullException.ThrowIfNullOrWhiteSpace(resourceKey);

        //    var pathSegments = resourceKey.Split('/');

        //    // Skip the first item and join the rest back into a string
        //    var itemKey = pathSegments[0];

        //    if (!this.Items.ContainsKey(itemKey)) { return null; }

        //    var item = this.Items[itemKey];

        //    // only one path segment?  then we just want the root item
        //    if (pathSegments.Length == 1)
        //    {
        //        return item;
        //    }
                        
        //    var subItemResourceKey = string.Join('/', pathSegments.Skip(1));

        //    return item.GetItem(subItemResourceKey);
        //}
    }
}
