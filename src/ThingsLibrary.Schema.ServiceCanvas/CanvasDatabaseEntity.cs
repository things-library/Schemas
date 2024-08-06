namespace ThingsLibrary.Schema.ServiceCanvas
{ 
    public class CanvasDatabaseEntitySchema
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Origin
        /// </summary>
        [Display(Name = "Database Type")]
        public string DatabaseType { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;


        /// <summary>
        /// If auto migrations should be disabled
        /// </summary>        
        public bool DisableMigrations { get; init; }

        /// <summary>
        /// If demo data should be seeded.
        /// </summary>
        public bool SeedTestData { get; init; }


        public CanvasDatabaseEntitySchema()
        {
            //nothing
        }
    }    
}
