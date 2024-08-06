namespace ThingsLibrary.Schema.ServiceCanvas
{
    /// <summary>
    /// Microservice Canvas Definition
    /// </summary>    
    public class CanvasSchema
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
        public Version? Version { get; init; } = typeof(CanvasSchema).Assembly.GetName().Version;

        /// <summary>
        /// Service Info
        /// </summary>
        public CanvasInfoSchema Info { get; set; } = new CanvasInfoSchema();

        /// <summary>
        /// Auth Dependency
        /// </summary>
        [Display(Name = "Auth Section")]
        public CanvasAuthSchema? Auth { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Database Details")]
        public CanvasDatabaseRelationalSchema? Database { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Entity Database Details")]
        public CanvasDatabaseEntitySchema? EntityDatabase { get; init; }

        /// <summary>
        /// Database Dependency
        /// </summary>
        [Display(Name = "Entity Database Details")]
        public CanvasMessageBusSchema? MessageBus{ get; init; }


        /// <summary>
        /// Storage Dependency
        /// </summary>
        [Display(Name = "Storage Details")]
        public CanvasStorageSchema? Storage { get; init; }

        /// <summary>
        /// Cache Dependency
        /// </summary>
        [Display(Name = "Redis Cache Details")]
        public CanvasCacheSchema? Cache { get; init; }

        /// <summary>
        /// Logging Dependency
        /// </summary>
        [Display(Name = "Logging Details")]
        public CanvasLoggingSchema? Logging { get; init; }

        /// <summary>
        /// Registered API dependencies for HttpClient registrations
        /// </summary>        
        [Display(Name = "Api")]
        public CanvasApiSchema? Api { get; init; }



        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasSchema()
        {
            //nothing
        }
    }
}
