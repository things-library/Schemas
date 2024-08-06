namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasInfoSchema
    {
        ///// <summary>
        ///// Section Id
        ///// </summary>
        //[ConfigurationKeyName("$id")]
        //[JsonPropertyName("$id")]
        //[Display(Name = "Section Id"), StringLength(200), Required]
        //public string Id => this.GetType().Name;

        /// <summary>
        /// Unique Namespace ID
        /// </summary>
        [Required, StringLength(200, ErrorMessage = "ID must be between 3 and 200 characters", MinimumLength = 3)]
        public string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// Title
        /// </summary>        
        [Display(Name = "Service Name"), StringLength(100), Required]
        public string Name { get; init; } = String.Empty;
        
        /// <summary>
        /// Description
        /// </summary>
        [Display(Name = "Description"), StringLength(500), Required]
        public string Description { get; init; } = String.Empty;

        /// <summary>
        /// Application / Group Name 
        /// </summary>
        [Display(Name = "Service Group Name"), StringLength(100)]
        public string GroupName { get; init; } = string.Empty;

        /// <summary>
        /// Assembly / App Version
        /// </summary>
        [Display(Name = "Version")]
        public Version? Version { get; init; } = new Version();
        

        /// <summary>
        /// DNS Hostname
        /// </summary>
        [Display(Name = "Host (Uri)"), DataType(DA.DataType.Url)]
        public Uri? Host { get; init; }

        /// <summary>
        /// Route Prefix / PathBase
        /// </summary>
        [Display(Name = "Route Prefix"), DataType(DA.DataType.Url)]
        public string? RoutePrefix { get; init; }
        
        /// <summary>
        /// Environment Name
        /// </summary>
        [Display(Name = "Environment Name"), StringLength(100), Required]
        public string Environment { get; init; } = string.Empty;

        /// <summary>
        /// Production Environment
        /// </summary>
        [JsonIgnore]
        public bool IsProduction => (string.Compare(this.Environment, "Production") == 0);

        /// <summary>
        /// Development Environment
        /// </summary>
        [JsonIgnore]
        public bool IsDevelopment => (string.Compare(this.Environment, "Development") == 0);

        /// <summary>
        /// Local (Localhost) Environment
        /// </summary>
        [JsonIgnore]
        public bool IsLocal => (string.Compare(this.Environment, "Local") == 0);

        /// <summary>
        /// Capabilities
        /// </summary>
        [Display(Name = "Capabilities", Description = "Comma delimited list of capabilities that this service provides."), StringLength(500), Required]
        public CanvasInfoCapabilitiesSchema Capabilities { get; init; } = new CanvasInfoCapabilitiesSchema();

        /// <summary>
        /// Capabilities
        /// </summary>
        [Display(Name = "Cors", Description = "CORS policies."), Required]
        public Dictionary<string, CanvasInfoCorsSchema> Cors { get; init; } = new Dictionary<string, CanvasInfoCorsSchema>();
          
        /// <summary>
        /// Open API Information
        /// </summary>
        [Display(Name = "Contact Information")]
        public CanvasInfoContactSchema? Contact { get; init; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public CanvasInfoSchema()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly() ?? throw new ArgumentException("Entry Assembly unknown.");

            this.Version = assembly.GetName().Version;
        }
    }
}
