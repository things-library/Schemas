// ================================================================================
// <copyright file="CanvasStorage.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{ 
    public class CanvasStorage
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;

        /// <summary>
        /// Storage Type
        /// </summary>
        /// <example>Azure_Blob</example>
        /// <remarks>Must be one of the following:
        ///     Azure_Blob
        ///     AWS_S3
        ///     GCP_Storage
        ///     Wasabi
        ///     Local
        /// </remarks>
        [Display(Name = "Storage Type"), StringLength(200), Required]
        public string StorageType { get; init; } = "Unknown";


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasStorage()
        {
            //nothing
        }
    }
}
