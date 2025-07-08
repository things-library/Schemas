// ================================================================================
// <copyright file="CanvasAuthJwt.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasAuthJwt
    {     
        /// <summary>        
        /// Open ID Provider Uri 
        /// </summary>
        /// <example>https://login.microsoftonline.com/{{tenant_id}}/v2.0</example>
        [DataType(DT.Url)]
        public Uri? Authority { get; init; }

        /// <summary>
        /// Open ID Config Uri (Typically: {authProviderUri}/.well-known/openid-configuration)
        /// </summary>
        public Uri? OpenIdConfigUri => (this.Authority != null ? new Uri(this.Authority, ".well-known/openid-configuration") : null);

        /// <summary>
        /// Issuer Uri
        /// </summary>
        public string? Issuer { get; init; }

        /// <summary>
        /// Audience
        /// </summary>
        public string? Audience { get; init; }

        /// <summary>
        /// Claim property
        /// </summary>
        public string NameClaimType { get; init; } = "name";

        /// <summary>
        /// Role Claim Type
        /// </summary>
        public string RoleClaimType { get; init; } = "roles";

        /// <summary>
        /// If JWT token validation should be disabled
        /// </summary>        
        public bool ShowValidationErrors { get; init; }

        /// <summary>
        /// If JWT token validation should be disabled
        /// </summary>
        public bool DisableValidation { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasAuthJwt()
        {
            //nothing
        }
    }
}
