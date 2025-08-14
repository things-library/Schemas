// ================================================================================
// <copyright file="CanvasCache.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{ 
    public class CanvasCache
    {
        /// <summary>
        /// Connection String variable name, empty string results in in-memory
        /// </summary>
        /// <example>ServiceCache</example>        
        [Display(Name = "ConnectionString Variable"), StringLength(200)]
        public string? ConnectionStringVariable { get; init; }

        /// <summary>
        /// Listing of items and their cache policy (timespan).  TimeSpan is serialized 00:00:00
        /// </summary>        
        [Display(Name = "Cache Policies"), StringLength(200), Required]
        public Dictionary<string, CanvasCachePolicy> CachePolicies { get; init; } = new Dictionary<string, CanvasCachePolicy>();

        /// <summary>
        /// Default cache policy (timespan).
        /// </summary> 
        [Display(Name = "Default Cache Duration"), Required]        
        public TimeSpan DefaultCacheDuration { get; init; }// = TimeSpan.FromMinutes(5);


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasCache() : base()
        {            
            //nothing
        }
    }    
}
