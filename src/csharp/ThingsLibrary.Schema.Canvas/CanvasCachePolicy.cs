// ================================================================================
// <copyright file="CanvasCachePolicy.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{ 
    public class CanvasCachePolicy
    {
        /// <summary>
        /// Type of caching policy
        /// </summary>        
        [Display(Name = "Policy Type (sliding, absolute)"), StringLength(50, MinimumLength = 1), Required]
        public string Type { get; init; } = string.Empty;

        [Required]
        public TimeSpan Duration { get; init; } = TimeSpan.FromSeconds(0);


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasCachePolicy() : base()
        {            
            //nothing
        }
    }    
}
