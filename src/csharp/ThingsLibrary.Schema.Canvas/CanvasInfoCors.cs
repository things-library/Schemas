// ================================================================================
// <copyright file="CanvasInfoCors.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasInfoCors
    {
        /// <summary>
        /// Origin
        /// </summary>
        [Display(Name = "Origin"), Required]
        public string Origin { get; init; } = "*";

        /// <summary>
        /// Methods
        /// </summary>
        [Display(Name = "Methods"), Required]
        public IEnumerable<string> Methods { get; init; } = new List<string>();

        /// <summary>
        /// Headers
        /// </summary>
        [Display(Name = "Headers"), Required]
        public IEnumerable<string> Headers { get; init; } = new List<string>();

    }
}
