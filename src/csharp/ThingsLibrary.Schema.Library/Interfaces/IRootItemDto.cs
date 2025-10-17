// ================================================================================
// <copyright file="IRootItemDto.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Interfaces
{
    public interface IRootItemDto : IItemDto
    {
        public string Key { get; set; }
    }
}