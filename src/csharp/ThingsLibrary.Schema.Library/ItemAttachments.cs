﻿// ================================================================================
// <copyright file="ItemAttachments.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

//namespace ThingsLibrary.Schema.Library
//{
//    /// <summary>
//    /// Basic Item Tags
//    /// </summary>
//    [DebuggerDisplay("({Items.Count} Attachments)")]
//    public class ItemAttachmentsDto : IEnumerable<ItemDto>
//    {
//        private IDictionary<string, ItemDto> Items { get; set; } = new Dictionary<string, ItemDto>();

//        /// <summary>
//        /// Accessor for the items
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public ItemDto? this[string key]
//        {
//            get => (this.Items.ContainsKey(key) ? this.Items[key] : null);

//            set
//            {
//                if (value == null)
//                {
//                    if (!this.Items.ContainsKey(key)) { return; }

//                    this.Items.Remove(key);
                    
//                    return;
//                } 

//                this.Items[key] = value;
//            }
//        }

//        /// <summary>
//        /// Add basic collection of tags to the listing
//        /// </summary>
//        /// <param name="tags">Flat listing of Item Basic Tags</param>
//        public void Add(ItemDto item)
//        {
//            //TODO: assign a key or throw a error?
//            ArgumentNullException.ThrowIfNull(item.Key);

//            // nothing to do?
//            if (item == null) { return; }
            
//            this.Items[item.Key] = item;
//        }

//        /// <summary>
//        /// Add basic collection of tags to the listing
//        /// </summary>
//        /// <param name="tags">Flat listing of Item Basic Tags</param>
//        public void Add(IEnumerable<ItemDto> attachments)
//        {
//            // nothing to do?
//            if (attachments == null) { return; }
                        
//            foreach (var attachment in attachments)
//            {
//                this.Add(attachment);
//            }
//        }
        
//        /// <summary>
//        /// Remove item (if exists) from collection
//        /// </summary>
//        /// <param name="key">Key</param>
//        /// <returns>True if item was found in collection, Fals if not found</returns>
//        public bool Remove(string key) => this.Items.Remove(key);

//        /// <summary>
//        /// Clear Listing
//        /// </summary>
//        public void Clear() => this.Items.Clear();

//        /// <summary>
//        /// Item Count
//        /// </summary>
//        public int Count => this.Items.Count;

//        #region --- IEnumerable ---

//        public IEnumerator<ItemDto> GetEnumerator()
//        {
//            // the items contain the 'key' field so we don't need to return keyvalue pairs
//            return this.Items.Values.ToList().GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.Items.Values.ToList().GetEnumerator();
//        }

//        #endregion
//    }
//}
