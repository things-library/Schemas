// ================================================================================
// <copyright file="CanvasMessageBus.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasMessageBus
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Message Bus Type
        /// </summary>
        public string Type { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;

        /// <summary>
        /// Send Channels
        /// </summary>
        public Dictionary<string, CanvasMessageBusChannel> SendChannels { get; init; } = new Dictionary<string, CanvasMessageBusChannel>();

        /// <summary>
        /// Receive Channels
        /// </summary>
        public Dictionary<string, CanvasMessageBusChannel> ReceiveChannels { get; init; } = new Dictionary<string, CanvasMessageBusChannel>();

        public CanvasMessageBus()
        {
            //nothing
        }
    }
}

