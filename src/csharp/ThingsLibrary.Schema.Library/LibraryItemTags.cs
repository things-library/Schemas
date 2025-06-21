// ================================================================================
// <copyright file="LibraryItemTags.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Collections;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Basic Item Tags
    /// </summary>
    [DebuggerDisplay("({Items.Count} Tags)")]
    public class LibraryItemTagsDto : IEnumerable<LibraryItemTagDto>
    {
        private Dictionary<string, LibraryItemTagDto> Items { get; set; } = [];
        
        /// <summary>
        /// Accessor for the items
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => (this.Items.ContainsKey(key) ? this.Items[key].Value : string.Empty);

            set
            {
                if (this.Items.ContainsKey(key))
                {
                    this.Items[key].Value = value;
                }
                else
                {
                    this.Items[key] = new LibraryItemTagDto(key, value);
                }
            }
        }

        /// <summary>
        /// To support serializer
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ToDictionary()
        {
            return this.Items.ToDictionary(x => x.Key, x => x.Value.Value);
        }

        /// <summary>
        /// Add basic collection of tags to the listing
        /// </summary>
        /// <param name="tags">Flat listing of Item Basic Tags</param>
        public void Add(LibraryItemTagsDto tags)
        {
            // nothing to do?
            if (tags == null) { return; }

            foreach (var tag in tags)
            {
                this.Add(tag);
            }
        }

        /// <summary>
        /// Add basic collection of tags to the listing
        /// </summary>
        /// <param name="tags">Flat listing of Item Basic Tags</param>
        public void Add(IEnumerable<LibraryItemTagDto> tags)
        {
            // nothing to do?
            if (tags == null) { return; }
                        
            foreach (var tag in tags)
            {
                this.Add(tag);
            }
        }

        /// <summary>
        /// Add basic tag to the listing
        /// </summary>
        /// <param name="tag">Flat listing of Item Basic Tags</param>
        public void Add(LibraryItemTagDto tag)
        {
            // nothing to add
            if(tag == null) { return; }
                        
            this.Items[tag.Key] = tag;                        
        }


        /// <summary>
        /// Add tag value to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(string key, string value)
        {
            this.Add(new LibraryItemTagDto(key, value));
        }

        /// <summary>
        /// Get Tag as T type
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultValue)
        {
            if (this.Items.TryGetValue(key, out LibraryItemTagDto? existingTag))
            {
                try
                {                    
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        return (T)converter.ConvertFromString(existingTag.Value);
                    }
                    else
                    {
                        return JsonSerializer.Deserialize<T>(existingTag.Value.ToLower()) ?? defaultValue;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.ToString());

                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Remove item (if exists) from collection
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if item was found in collection, Fals if not found</returns>
        public bool Remove(string key) => this.Items.Remove(key);

        /// <summary>
        /// Clear Listing
        /// </summary>
        public void Clear() => this.Items.Clear();

        /// <summary>
        /// Item Count
        /// </summary>
        public int Count => this.Items.Count;

        #region --- IEnumerable ---

        public IEnumerator<LibraryItemTagDto> GetEnumerator()
        {
            // the items contain the 'key' field so we don't need to return keyvalue pairs
            return this.Items.Values.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.Values.ToList().GetEnumerator();
        }

        #endregion
    }
}
