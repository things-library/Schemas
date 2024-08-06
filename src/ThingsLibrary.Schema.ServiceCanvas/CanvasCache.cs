namespace ThingsLibrary.Schema.ServiceCanvas
{ 
    public class CanvasCacheSchema
    {
        /// <summary>
        /// Connection String variable name, empty string results in in-memory
        /// </summary>
        /// <example>ServiceCache</example>        
        [Display(Name = "ConnectionString Variable"), StringLength(200)]
        public string? ConnectionStringVariable { get; init; }

        /// <summary>
        /// Listing of items and their cache policy (timespan).  TimeSpan is serialized 00:00:00
        /// </summary>        
        [Display(Name = "Cache Policies"), StringLength(200), Required]
        public Dictionary<string, TimeSpan> CachePolicies { get; init; } = new Dictionary<string, TimeSpan>();

        /// <summary>
        /// Default cache policy (timespan).
        /// </summary> 
        [Display(Name = "Default Cache Duration"), Required]        
        public TimeSpan DefaultCacheDuration { get; init; }// = TimeSpan.FromMinutes(5);


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasCacheSchema() : base()
        {            
            //nothing
        }
    }    
}
