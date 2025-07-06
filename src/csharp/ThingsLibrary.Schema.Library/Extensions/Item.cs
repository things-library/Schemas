// ================================================================================
// <copyright file="Item.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class ItemExtensions
    {
        public static string GetTypeTagName(this RootItemDto rootItemDto, string typeKey, string tagKey)
        {
            if (!rootItemDto.Types.ContainsKey(typeKey)) { return tagKey; }

            var type = rootItemDto.Types[typeKey];
            if (!type.Tags.ContainsKey(tagKey)) { return tagKey; }

            return type.Tags[tagKey].Name;
        }

        public static short GetTypeTagWeight(this RootItemDto rootItemDto, string typeKey, string tagKey)
        {
            if (!rootItemDto.Types.ContainsKey(typeKey)) { return 0; }

            var type = rootItemDto.Types[typeKey];
            if (!type.Tags.ContainsKey(tagKey)) { return 0; }

            return type.Tags[tagKey].Weight;
        }        
    }
}
