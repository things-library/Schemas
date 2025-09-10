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
        public static void ApplyDefinition(this ItemTypeDto itemType, ItemTypeDto definitionType)
        {
            itemType.Name = itemType.Name;
            
            // replace the tags with their definitions
            foreach (var tagKey in itemType.Tags.Keys)
            {
                if (!definitionType.Tags.ContainsKey(tagKey)) { continue; }

                itemType.Tags[tagKey] = definitionType.Tags[tagKey].Clone();
            }

            // copy over the meta data (don't replace any already existing items)            
            foreach (var tag in definitionType.Meta)
            {
                itemType.Meta[tag.Key] = tag.Value;
            }
        }
    }
}
