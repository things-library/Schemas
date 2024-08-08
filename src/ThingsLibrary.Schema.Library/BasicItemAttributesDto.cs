using System;
using System.Collections;
using System.Collections.Generic;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Basic Item Attributes
    /// </summary>
    [DebuggerDisplay("(Qty: {Items.Count})")]
    public class BasicItemAttributesDto : IEnumerable<BasicItemAttributeDto>
    {
        private Dictionary<string, BasicItemAttributeDto> Items { get; set; } = [];

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
                    this.Items[key] = new BasicItemAttributeDto(key, value);
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
        public void Add(BasicItemAttributesDto attributes, bool append = false)
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
        public void Add(IEnumerable<BasicItemAttributeDto> attributes, bool append = false)
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
        public void Add(BasicItemAttributeDto attribute, bool append = false)
        {
            // nothing to add
            if(attribute == null) { return; }

            // see if it already exists
            if (this.Items.TryGetValue(attribute.Key, out BasicItemAttributeDto? existingAttribute))
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
                        existingAttribute.Value = attribute.Values[0];
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
            this.Add(new BasicItemAttributeDto(key, value), append);
        }

        /// <summary>
        /// Add attribute values list to the listing
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">Values</param>
        /// <param name="append">If value should be appended if not in the list</param>
        public void Add(string key, List<string> values, bool append = false)
        {
            this.Add(new BasicItemAttributeDto(key, values), append);
        }

        #region --- IEnumerable ---
        
        public IEnumerator<BasicItemAttributeDto> GetEnumerator()
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
