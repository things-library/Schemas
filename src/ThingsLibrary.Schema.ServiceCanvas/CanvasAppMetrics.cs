namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasAppMetricsSchema
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceAppInsights</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;


        public CanvasAppMetricsSchema() 
        {
            //nothing
        }
    }
}
