// ================================================================================
// <copyright file="CanvasApi.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasApi
    {
        /// <summary>
        /// Uri to the OpenApi/Swagger Doc file
        /// </summary>        
        [Display(Name = "API Endpoints"), Required]
        public Dictionary<string, CanvasApiEndpoint> Endpoints { get; set; } = new Dictionary<string, CanvasApiEndpoint>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasApi()
        {
            //nothing
        }
    }

    public class CanvasApiEndpoint
    {
        /// <summary>
        /// Base Uri to the API Service
        /// </summary>        
        [Display(Name = "Base Url")]
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Default Headers
        /// </summary>        
        [Display(Name = "Headers"), Required]
        public Dictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();

        /// <summary>
        /// Service Options and settings, account ids, account keys
        /// </summary>        
        [Display(Name = "Options"), Required]
        public Dictionary<string, string> Options { get; init; } = new Dictionary<string, string>();

        /// <summary>
        /// Uri to the OpenApi/Swagger Doc file
        /// </summary>        
        [Display(Name = "Open API Url")]
        public string? OpenApiUrl { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>              
        public string ClientId { get; init; } = string.Empty;

        /// <summary>
        /// Client Secret
        /// </summary>
        [JsonIgnore]        
        public string ClientSecret { get; init; } = string.Empty;

        /// <summary>
        /// Scope
        /// </summary>
        /// <remarks>OIDC scope must contain 'openid' and usually contains 'profile' and 'email'.  The 'offline_access' scopes allows for token refreshing.</remarks>        
        public List<string> Scope { get; init; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasApiEndpoint()
        {
            //nothing
        }
    }
}
