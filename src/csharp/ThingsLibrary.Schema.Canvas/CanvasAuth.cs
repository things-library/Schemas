// ================================================================================
// <copyright file="CanvasAuth.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    /// <summary>
    /// Canvas Auth
    /// </summary>
    public class CanvasAuth
    {
        /// <summary>
        /// Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Url to use for login
        /// </summary>
        [Required]
        public string LoginUrl { get; init; } = string.Empty;

        /// <summary>
        /// Url to use for logout
        /// </summary>
        [Required]
        public string LogoutUrl { get; init; } = string.Empty;

        /// <summary>
        /// Url to use for access denied (401)
        /// </summary>        
        public string AccessDeniedUrl { get; init; } = string.Empty;

        /// <summary>
        /// The ReturnUrlParameter determines the name of the query string parameter which is appended by the handler during a Challenge.
        /// </summary>
        /// <remarks>Defaults to 'returnUrl'</remarks>        
        public string ReturnUrlParameter { get; init; } = "redirect_uri";

        /// <summary>
        /// How long the auth ticket stored in the cookie will remain valid after creation.
        /// </summary>
        /// <remarks>Defaults to 60 min</remarks>        
        public TimeSpan ExpireTimeSpan { get; init; } = TimeSpan.FromSeconds(60); //60 min

        /// <summary>
        /// The SlidingExpiration is set to true to instruct the handler to re-issue a new cookie with a new
        /// expiration time any time it processes a request which is more than halfway through the expiration window.
        /// </summary>
        public bool SlidingExpiration { get; init; } = false;

        /// <summary>
        /// Allow requests to be anonymous to hit endpoints of this service, otherwise [AllowAnonymous] attributes MUST be used
        /// </summary>
        public bool AllowAnonymousRequests { get; set; }

        /// <summary>
        /// Claims Mapping (map of system expected 'role' to actual role from auth provider)
        /// </summary>
        public Dictionary<string, List<string>> PolicyClaimsMap { get; init; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Expected App Roles the system uses.
        /// </summary>
        public List<string> AppRoles { get; init; } = new List<string>();

        /// <summary>
        /// JWT Token Details
        /// </summary>
        [Required]
        public CanvasAuthJwt? Jwt { get; init; } = null;

        /// <summary>
        /// Open ID Connect Details
        /// </summary>
        public CanvasAuthOpenId? OpenId { get; init; } = null;

        /// <summary>
        /// Cookie details
        /// </summary>
        public CanvasAuthCookie? Cookie { get; init; } = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasAuth() : base()
        {
            //nothing
        }
    }
}
