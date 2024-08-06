namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasApiSchema
    {
        /// <summary>
        /// Uri to the OpenApi/Swagger Doc file
        /// </summary>        
        [Display(Name = "API Endpoints"), Required]
        public Dictionary<string, CanvasApiEndpointSchema> Endpoints { get; set; } = new Dictionary<string, CanvasApiEndpointSchema>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasApiSchema()
        {
            //nothing
        }
    }

    public class CanvasApiEndpointSchema
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
        public Dictionary<string, string> Headers { get; init; } = [];

        /// <summary>
        /// Service Options and settings, account ids, account keys
        /// </summary>        
        [Display(Name = "Options"), Required]
        public Dictionary<string, string> Options { get; init; } = [];

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
        public List<string> Scope { get; init; } = [];

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasApiEndpointSchema()
        {
            //nothing
        }
    }
}
