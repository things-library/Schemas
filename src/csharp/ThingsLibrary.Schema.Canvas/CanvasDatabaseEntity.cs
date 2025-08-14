// ================================================================================
// <copyright file="CanvasDatabaseEntity.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{ 
    public class CanvasDatabaseEntity
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200, MinimumLength = 1), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Database Name
        /// </summary>
        [Display(Name = "Database Name"), StringLength(200, MinimumLength = 1), Required]
        public string DatabaseName { get; init; } = string.Empty;

        /// <summary>
        /// Origin
        /// </summary>
        [Display(Name = "Database Type")]
        public string DatabaseType { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;


        /// <summary>
        /// If auto migrations should be disabled
        /// </summary>        
        public bool DisableMigrations { get; init; }

        /// <summary>
        /// If demo data should be seeded.
        /// </summary>
        public bool SeedTestData { get; init; }


        public CanvasDatabaseEntity()
        {
            //nothing
        }
    }    
}
