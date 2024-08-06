namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasInfoCapabilitiesSchema
    {
        /// <summary>
        /// Swagger / OpenAPI
        /// </summary>
        [Display(Name = "Swagger"), Required]
        public bool Swagger { get; init; }

        /// <summary>
        /// Health Checks , default is true.
        /// </summary>
        /// <remarks>
        /// Health Check Endpoints:
        ///   /health
        ///   /health/ready
        ///   /health/startup
        /// </remarks>
        [Display(Name = "HealthChecks"), Required]
        public bool HealthChecks { get; init; } = true;

        /// <summary>
        /// Controllers
        /// </summary>
        [Display(Name = "Controllers"), Required]
        public bool Controllers { get; init; }

        /// <summary>
        /// Razor Pages
        /// </summary>
        [Display(Name = "Razor Pages"), Required]
        public bool RazorPages { get; init; }

        /// <summary>
        /// Server Side Blazer
        /// </summary>
        [Display(Name = "Server Side Blazor"), Required]
        public bool ServerSideBlazor { get; init; }

        /// <summary>
        /// Static Files
        /// </summary>
        [Display(Name = "Static Files"), Required]
        public bool StaticFiles { get; init; }
          
        /// <summary>
        /// MVC
        /// </summary>
        [Display(Name = "Model View Controller (MVC)"), Required]
        public bool Mvc { get; init; }

        /// <summary>
        /// Http Redirection to Https
        /// </summary>
        [Display(Name = "Http Redirection"), Required]
        public bool HttpRedirection { get; init; }

        /// <summary>
        /// Http Redirection to Https
        /// </summary>
        [Display(Name = "Response Compression"), Required]
        public bool ResponseCompression { get; init; }
    }
}
