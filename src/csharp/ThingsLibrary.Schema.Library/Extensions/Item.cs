// ================================================================================
// <copyright file="Item.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Get all types in use for the item recursive
        /// </summary>
        /// <param name="item">Item to traverse</param>
        /// <returns></returns>
        public static List<string> GetAllTypes(this ItemDto item)
        {
            var list = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            GetAllTypes(item, list);
            
            return list.ToList();
        }

        private static void GetAllTypes(ItemDto item, HashSet<string> list)
        {      
            list.Add(item.Type);

            foreach (var childItem in item.Items.Values)
            {
                GetAllTypes(childItem, list);
            }            
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, string? value)
        {
            if (value == null || string.IsNullOrEmpty(value)) { return; }

            item.Tags[tagName] = value;
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, double? value)
        {
            if (value == null) { return; }

            item.Tags[tagName] = $"{value}";
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, DateTime? value)
        {
            if (value == null) { return; }

            item.Tags[tagName] = $"{value:O}";
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, Guid? value)
        {
            if (value == null) { return; }

            item.Tags[tagName] = $"{value}";
        }

        /// <summary>
        /// Set the metadata value if provided value is set
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tagName"></param>
        /// <param name="value"></param>
        public static void SetMetaIfNotNull(this ItemDto item, string tagName, string? value)
        {
            if (value == null || string.IsNullOrEmpty(value)) { return; }

            item.Meta[tagName] = value;
        }        
    }
}
