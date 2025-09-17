// ================================================================================
// <copyright file="ItemType.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Library.Base;

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class ItemTypeExtensions
    {
        public static bool IsInvalid(this ItemTypeDto itemTypeDto)
        {
            // quick and dirty check
            return (string.IsNullOrEmpty(itemTypeDto.Name));
        }

        public static void ApplyDefinition(this ItemTypeDto itemType, ItemTypeDto definitionType)
        {
            itemType.Name = definitionType.Name;
            
            // replace the tags with their definitions
            foreach (var tagKey in itemType.Tags.Keys)
            {
                if (!definitionType.Tags.ContainsKey(tagKey)) { continue; }

                itemType.Tags[tagKey] = definitionType.Tags[tagKey].Clone();
            }

            // copy over the meta data (don't replace any already existing items)            
            foreach (var definitionTag in definitionType.Meta)
            {
                itemType.Meta[definitionTag.Key] = definitionTag.Value;
            }
        }
    }
}
