// ================================================================================
// <copyright file="CanvasMessageBusChannel.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasMessageBusChannel
    {
        /// <summary>
        /// Payload Entity Schema / Namespace
        /// </summary>
        [Display(Name = "Address"), StringLength(200), Required]
        public string Address { get; init; } = string.Empty;
    }
}

