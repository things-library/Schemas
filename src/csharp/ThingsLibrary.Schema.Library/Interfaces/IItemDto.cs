// ================================================================================
// <copyright file="IItemDto.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Interfaces
{
    public interface IItemDto
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public IDictionary<string, string> Meta { get; set; }

        public IDictionary<string, string> Tags { get; set; }

        public string? this[string key, bool isMeta = false] { get; }
    }
}