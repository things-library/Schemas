// ================================================================================
// <copyright file="Canvas.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    /// <summary>
    /// Microservice Canvas Definition
    /// </summary>    
    public class CanvasRoot
    {
        /// <summary>
        /// Service Canvas Version
        /// </summary>        
        /// <remarks>
        /// Semantic Versioning (https://semver.org/)
        /// MAJOR.MINOR.PATCH.BUILD
        /// - MAJOR is incremented when you make incompatible API changes
        /// - MINOR is incremented when you add functionality in a backwards-compatible manner
        /// - PATCH is incremented when you make backwards-compatible bug fixes
        /// </remarks>                
        public Version? Version { get; init; } = typeof(CanvasRoot).Assembly.GetName().Version;

        /// <summary>
        /// Service Info
        /// </summary>
        [Required]
        public CanvasInfo Info { get; init; } = new CanvasInfo();

        /// <summary>
        /// Auth Dependency
        /// </summary>
        [Display(Name = "Auth Section")]
        public CanvasAuth? Auth { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Database Details")]
        public CanvasDatabaseRelational? Database { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Entity Database Details")]
        public CanvasDatabaseEntity? EntityDatabase { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Entity Database Details")]
        public CanvasMessageBus? MessageBus { get; init; }


        /// <summary>
        /// Storage Dependency
        /// </summary>
        [Display(Name = "Storage Details")]
        public CanvasStorage? Storage { get; init; }

        /// <summary>
        /// Cache Dependency
        /// </summary>
        [Display(Name = "Redis Cache Details")]
        public CanvasCache? Cache { get; init; }

        /// <summary>
        /// Logging Dependency
        /// </summary>
        [Display(Name = "Logging Details")]
        public CanvasLogging? Logging { get; init; }

        /// <summary>
        /// Notifications Dependency
        /// </summary>
        [Display(Name = "Notification Details")]
        public CanvasNotification? Notification { get; init; }

        /// <summary>
        /// Registered API dependencies for HttpClient registrations
        /// </summary>        
        [Display(Name = "Api")]
        public CanvasApi? Api { get; init; }



        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasRoot()
        {
            //nothing
        }
    }
}
