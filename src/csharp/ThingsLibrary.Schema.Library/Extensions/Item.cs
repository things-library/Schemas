// ================================================================================
// <copyright file="Item.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Diagnostics.CodeAnalysis;

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Try to get the item at the specified key path
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool TryGetItem(this ItemDto rootItem, string key, [MaybeNullWhen(false)] out ItemDto item, bool required = false)
        {
            var keyPath = key.Split('/');

            // nested item
            if (rootItem.Items.TryGetValue(keyPath[0], out var currentItem))
            {
                // end of key path
                if (keyPath.Length == 1)
                {
                    item = currentItem;
                    return true;
                }
                else
                {
                    var remainingKey = string.Join('/', keyPath.Skip(1));

                    // recurse to find the item
                    return currentItem.TryGetItem(remainingKey, out item);
                }
            }
            else if (required)
            {
                throw new ArgumentException($"Unable to find required item with key: {key}");
            }
            else
            {
                // unable to find at this branch level
                item = null;
                return false;
            }
        }


        /// <summary>
        /// Try to get the tag value from a specified item
        /// </summary>
        /// <param name="rootItem">Root Item</param>
        /// <param name="itemKey">Item Key path</param>
        /// <param name="tagKey">Tag Key</param>
        /// <param name="tagValue">Parsed tag value</param>
        /// <returns></returns>
        public static bool TryGetItemTag(this ItemDto rootItem, string itemKey, string tagKey, [MaybeNullWhen(false)] out string tagValue)
        {
            if (rootItem.TryGetItem(itemKey, out var item) && item.Tags.TryGetValue(tagKey, out tagValue))
            {
                return true;
            }
            else
            {
                tagValue = null;
                return false;
            }
        }

        public static bool IsInvalid(this RootItemDto itemDto)
        {
            // quick and dirty check
            if (!string.IsNullOrEmpty(itemDto.Key)) { return false; }

            return ((ItemDto)itemDto).IsInvalid();
        }

        public static bool IsInvalid(this ItemDto itemDto)
        {
            // quick and dirty check
            return string.IsNullOrEmpty(itemDto.Type) || string.IsNullOrEmpty(itemDto.Name);
        }

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


        public static void Merge(this RootItemDto libraryDto, RootItemDto itemDto)
        {
            if(itemDto.Type != Constants.TYPE_LIBRARY_KEY)
            {
                libraryDto.Items.Add(itemDto.Key, itemDto);
            }
            else
            {
                foreach(var childItemDto in itemDto.Items)
                {
                    libraryDto.Items.Add(childItemDto.Key, childItemDto.Value);
                }
            }
        }

        #region --- SetTagIfNotNull ---

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
        /// <param name="precision">Precision for formatting the value (optional)</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, double? value, short? precision = null)
        {
            if (value == null) { return; }

            if (precision != null)
            {
                var format = $"D{precision}";
                item.Tags[tagName] = string.Format($"{{0:{format}}}", value);
            }
            else
            {
                item.Tags[tagName] = $"{value}";
            }
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTagIfNotNull(this ItemDto item, string tagName, decimal? value, int? precision = null)
        {
            if (value == null) { return; }

            if (precision != null)
            {
                item.Tags[tagName] = $"{decimal.Round(value.Value, precision.Value)}";
            }
            else
            {
                item.Tags[tagName] = $"{value}";
            }
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
        public static void SetTagIfNotNull(this ItemDto item, string tagName, DateOnly? value)
        {
            if (value == null) { return; }

            item.Tags[tagName] = $"{value:yyyy-MM-dd}";
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

        #endregion

        #region --- SetTag ---

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTag(this ItemDto item, string tagName, string? value)
        {
            if (value == null || string.IsNullOrEmpty(value)) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            item.Tags[tagName] = value;
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        /// <param name="precision">Precision for formatting the value (optional)</param>
        public static void SetTag(this ItemDto item, string tagName, double? value, short? precision = null)
        {
            if (value == null) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            if (precision != null)
            {
                var format = $"D{precision}";
                item.Tags[tagName] = string.Format($"{{0:{format}}}", value);
            }
            else
            {
                item.Tags[tagName] = $"{value}";
            }
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTag(this ItemDto item, string tagName, decimal? value, int? precision = null)
        {
            if (value == null) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            if (precision != null)
            {
                item.Tags[tagName] = $"{decimal.Round(value.Value, precision.Value)}";
            }
            else
            {
                item.Tags[tagName] = $"{value}";
            }
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        /// <param name="minValidValue">If left null, mindate will be 1/1/2000</param>
        public static void SetTag(this ItemDto item, string tagName, DateTime? value, DateTime? minValidValue = null)
        {
            // set to some reasonable min date if not provided
            if(minValidValue == null) 
            {
                minValidValue = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }

            if (value == null || value < minValidValue) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            item.Tags[tagName] = $"{value:O}";
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTag(this ItemDto item, string tagName, DateOnly? value)
        {
            if (value == null)
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            item.Tags[tagName] = $"{value:yyyy-MM-dd}";
        }

        /// <summary>
        /// Set the tag if the provided value is set
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="tagName">Tag Name</param>
        /// <param name="value">Value</param>
        public static void SetTag(this ItemDto item, string tagName, Guid? value)
        {
            if (value == null) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            item.Tags[tagName] = $"{value}";
        }

        #endregion

        /// <summary>
        /// Set the metadata value if provided value is set
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tagName"></param>
        /// <param name="value"></param>
        public static void SetMetaIfNotNull(this ItemDto item, string tagName, string? value)
        {
            if (value == null || string.IsNullOrEmpty(value)) 
            {
                item.Tags[tagName] = string.Empty;
                return;
            }

            item.Meta[tagName] = value;
        }
               
        /// <summary>
        /// Flatten the item hierarchy perserving the full key path (clearing the child items of each)  (AKA: use a clone if you don't want the source to be modified)
        /// </summary>
        /// <param name="itemDto">Item</param>
        /// <param name="itemResourceKey">Item Resource Key (aka: full path)</param>
        /// <returns>Returns a listing of item resource keys and their item</returns>
        public static ICollection<KeyValuePair<string, ItemDto>> Flatten(this ItemDto itemDto, string itemResourceKey)
        {
            if (string.IsNullOrEmpty(itemResourceKey)) { throw new ArgumentException("Resource Key missing."); }

            var items = new List<KeyValuePair<string, ItemDto>>();

            // if we are dealing with a library container.. add the item
            if (itemResourceKey.Contains('/')) // no paths outside of root?  == library container            
            { 
                items.Add(new KeyValuePair<string, ItemDto>(itemResourceKey, itemDto));                
            }

            foreach (var child in itemDto.Items)
            {
                items.AddRange(child.Value.Flatten($"{itemResourceKey}/{child.Key}"));
            }

            return items;
        }

        #region --- Data Validation ---

        /// <summary>
        /// Validate the data that is provided and if it is valid
        /// </summary>
        /// <param name="itemTypeTag"></param>
        /// <param name="tagValue"></param>
        /// <returns></returns>
        public static bool IsDataValid(this ItemTypeTagDto itemTypeTag, string tagValue)
        {
            return ItemTagDataTypesDto.IsValid(itemTypeTag.Type, tagValue);
        }


        /// <summary>
        /// Validate all the items against the library definitions
        /// </summary>
        /// <param name="libraryDto">Library Definitions</param>
        /// <param name="items">Items to validate</param>
        /// <returns>Collection of validation results</returns>
        public static ICollection<ValidationResult> Validate(this RootItemDto libraryDto, ICollection<KeyValuePair<string, ItemDto>> items)
        {
            var results = new List<ValidationResult>();
            foreach (var importItem in items)
            {
                var validationError = libraryDto.Validate(importItem.Key, importItem.Value);
                if (validationError != null)
                {
                    results.Add(validationError);
                }
            }
            return results;
        }

        /// <summary>
        /// Validate all the items against the library definitions
        /// </summary>
        /// <param name="libraryDto">Library Definitions</param>
        /// <param name="items">Items to validate</param>
        /// <returns>Collection of validation results</returns>
        public static ICollection<ValidationResult> Validate(this RootItemDto libraryDto, IDictionary<string, ItemDto> items)
        {
            var results = new List<ValidationResult>();
            foreach (var importItem in items)
            {
                var validationError = libraryDto.Validate(importItem.Key, importItem.Value);
                if (validationError != null)
                {
                    results.Add(validationError);
                }
            }
            return results;
        }

        /// <summary>
        /// Validate the item against the library definitions
        /// </summary>
        /// <param name="libraryDto">Library Definitions</param>
        /// <param name="itemKey">Item Key</param>
        /// <param name="itemDto">Item to evaluate</param>
        /// <returns>Validation result if there is a validation issue, otherwise null</returns>
        public static ValidationResult? Validate(this RootItemDto libraryDto, string itemKey, ItemDto itemDto)
        {
            // is this type in the library?
            if (libraryDto.Types.TryGetValue(itemDto.Type, out var itemType))
            {
                foreach (var itemTag in itemDto.Tags)
                {
                    // is this type tag in the library?
                    if (itemType.Tags.TryGetValue(itemTag.Key, out var itemTypeTag))
                    {
                        // is the data valid for this tag?
                        if (!itemTypeTag.IsDataValid(itemTag.Value))
                        {
                            return new ValidationResult($"Invalid data type '{itemTypeTag.Type}'", new List<string> { $"{itemKey}.tags['{itemTag.Key}']" });
                        }
                    }
                    else
                    {
                        return new ValidationResult($"Missing tag definition", new List<string> { $"{itemKey}.tags['{itemTag.Key}']" });
                    }
                }
            }
            else
            {
                return new ValidationResult($"Missing definition for type '{itemDto.Type}'.", new List<string> { $"{itemKey}.type" });
            }

            // no validation errors
            return null;
        }

        #endregion
    }
}
