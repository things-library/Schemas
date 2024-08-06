namespace ThingsLibrary.Schema.ServiceCanvas
{ 
    public class CanvasLoggingSchema
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceCache</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;

        /// <summary>
        /// Container name that holds the logs
        /// </summary>
        public string ContainerName { get; init; } = "Logs";

        /// <summary>
        /// File naming pattern to use
        /// </summary>
        public string FileNamePattern { get; init; } = "{yyyy}-{MM}/{dd}/{yyyy}-{MM}-{dd}_{HH}:{mm}.txt";


        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasLoggingSchema() : base()
        {            
            //nothing
        }
    }    
}
