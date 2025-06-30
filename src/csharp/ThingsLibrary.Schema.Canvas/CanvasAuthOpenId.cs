// ================================================================================
// <copyright file="CanvasAuthOpenId.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasAuthOpenId
    {
        /// <summary>
        /// Open ID Provider Uri 
        /// </summary>
        /// <example>https://login.microsoftonline.com/{{tenant_id}}/v2.0</example>
        [Required]
        public Uri Authority { get; init; }  = new Uri("about:blank");

        /// <summary>
        /// Open ID Config Uri (Typically: {authProviderUri}/.well-known/openid-configuration)
        /// </summary>
        public Uri OpenIdConfigUri => new Uri(this.Authority, ".well-known/openid-configuration");

        /// <summary>
        /// Client ID
        /// </summary>        
        [Required]
        public string ClientId { get; init; } = string.Empty;

        /// <summary>
        /// Client Secret
        /// </summary>
        [JsonIgnore]
        [Required]
        public string ClientSecret { get; init; } = string.Empty;

        /// <summary>
        /// Callback Uri
        /// </summary>        
        [Required]
        public string CallbackUrl { get; init; } = string.Empty;

        /// <summary>
        /// Scope
        /// </summary>
        /// <remarks>OIDC scope must contain 'openid' and usually contains 'profile' and 'email'.  The 'offline_access' scopes allows for token refreshing.</remarks>        
        public List<string> Scope { get; init; } = new List<string>();

        /// <summary>
        /// If the handler should go and get additional claims from the UserInfo endpoint
        /// </summary>        
        public bool UseUserInfoForClaims { get; init; }

        /// <summary>
        /// Enables or disables the use of the Proof Key for Code Exchange (PKCE) standard. Default value is true.
        /// </summary>        
        public bool UsePkce { get; init; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasAuthOpenId() : base()
        {
            //nothing
        }
    }
}
