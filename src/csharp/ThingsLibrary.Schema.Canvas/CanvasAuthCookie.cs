// ================================================================================
// <copyright file="CanvasAuthCookie.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasAuthCookie
    {
        /// <summary>
        /// Cookie Name
        /// </summary>
        [Required, StringLength(200, ErrorMessage = "Name must be between 3 and 200 characters", MinimumLength = 3)]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Cookie Path 
        /// </summary>
        public string? Path { get; init; }

        /// <summary>
        /// Cookie Domain
        /// </summary>
        public string? Domain { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasAuthCookie()
        {
            //nothing
        }
    }
}
