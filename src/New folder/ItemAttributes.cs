using System.Collections;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Basic Item Attributes
    /// </summary>
    [DebuggerDisplay("({Items.Count} Attributes)")]
    public class ItemAttributesDto : IEnumerable<ItemAttributeDto>
    {
        private Dictionary<string, ItemAttributeDto> Items { get; set; } = [];
        
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
                    this.Items[key] = new ItemAttributeDto(key, value);
                }
            }
        }

        /// <summary>
        /// To support serializer
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return this.Items.ToDictionary(x => x.Key, x => x.Value.Values.Count > 1 ? x.Value.Values : (object)x.Value.Value);
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        public void Add(ItemAttributesDto attributes, bool append = false)
        {
            // nothing to do?
            if (attributes == null) { return; }

            foreach (var attribute in attributes)
            {
                this.Add(attribute);
            }
        }

        /// <summary>
        /// Add basic collection of attributes to the listing
        /// </summary>
        /// <param name="attributes">Flat listing of Item Basic Attributes</param>
        public void Add(IEnumerable<ItemAttributeDto> attributes, bool append = false)
        {
            // nothing to do?
            if (attributes == null) { return; }
                        
            foreach (var attribute in attributes)
            {
                this.Add(attribute, append);
            }
        }

        /// <summary>
        /// Add basic attribute to the listing
        /// </summary>
        /// <param name="attribute">Flat listing of Item Basic Attributes</param>
        public void Add(ItemAttributeDto attribute, bool append = false)
        {
            // nothing to add
            if(attribute == null) { return; }

            // see if it already exists
            if (this.Items.TryGetValue(attribute.Key, out ItemAttributeDto? existingAttribute))
            {
                foreach (var value in attribute.Values)
                {
                    // 1 to many
                    if (append)
                    {
                        if (!existingAttribute.Values.Contains(value))
                        {
                            attribute.Values.Add(value);
                        }
                    }
                    else
                    {
                        // replace all values
                        existingAttribute.Values = attribute.Values;
                    }
                }
            }            
            else
            {
                this.Items[attribute.Key] = attribute;
            }            
        }


        /// <summary>
        /// Add attribute value to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="append">If value should be appended if not in the list</param>
        public void Add(string key, string value, bool append = false)
        {
            this.Add(new ItemAttributeDto(key, value), append);
        }

        /// <summary>
        /// Add attribute value to listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="append">If value should be appended if not in the list</param>
        public void Add(string key, object value, bool append = false)
        {
            this.Add(new ItemAttributeDto(key, value), append);
        }

        /// <summary>
        /// Add attribute values list to the listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">Values</param>
        /// <param name="append">If value should be appended if not in the list</param>
        public void Add(string key, List<string> values, bool append = false)
        {
            this.Add(new ItemAttributeDto(key, values), append);
        }

        /// <summary>
        /// Get Attribute as T type
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultValue)
        {
            if (this.Items.TryGetValue(key, out ItemAttributeDto? existingAttribute))
            {
                try
                {
                    if(defaultValue is TimeSpan || defaultValue is bool)
                    {                        
                        var converter = TypeDescriptor.GetConverter(typeof(T));
                        if (converter != null)
                        {
                            return (T)(converter.ConvertFromString(existingAttribute.Value) ?? defaultValue);
                        }                 
                    }
                                       
                    return JsonSerializer.Deserialize<T>(existingAttribute.Value.ToLower()) ?? defaultValue;
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

        public IEnumerator<ItemAttributeDto> GetEnumerator()
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
