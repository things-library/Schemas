
namespace ThingsLibrary.Schema.Base
{
    /// <summary>
    /// Base Schema Attributes
    /// </summary>
    public class SchemaBase
    {
        #region --- STATIC ---

        // Should be sent with mime type: "application/schema+json"

        /// <summary>
        /// Version string
        /// </summary>
        public const string CurrentVersion = "1.0";

        /// <summary>
        /// Json Schema Definition
        /// </summary>
        public static string BaseUrl { get; } = $"https://schema.thingslibrary.io/{CurrentVersion}";

        /// <summary>
        /// Pattern to use for all non-root library keys
        /// </summary>
        public const string KeyPattern = "^[a-z0-9_]*$";

        /// <summary>
        /// Key pattern description
        /// </summary>
        public const string KeyPatternDescription = "Invalid Characters.  Please only use lowercase letters, numeric, and underscores.";

        /// <summary>
        /// Pattern to use for library and item keys
        /// </summary>
        public const string RootKeyPattern = "^[a-z0-9_\\-]*$";

        /// <summary>
        /// Description of the pattern for users
        /// </summary>
        public const string RootKeyPatternDescription = "Invalid Characters.  Please only use lowercase letters, numeric, underscores and hyphens.";

        /// <summary>
        /// Current Version
        /// </summary>
        public static Version Version { get; } = new Version(CurrentVersion);

        /// <summary>
        /// Standard json serialization settings for our libraray objects
        /// </summary>        
        public static JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { JsonIgnoreEmptyCollection.IgnoreEmptyCollections }
            }
        };

        #endregion

        /// <summary>
        /// Json Schema Definition
        /// </summary>
        [JsonPropertyName("$schema"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri? SchemaUrl { get; set; }

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("metadata"), JsonIgnoreEmptyCollection]
        public Dictionary<string, string> Metadata { get; set; } = new();


        ///// <summary>
        ///// Get Schema Version
        ///// </summary>
        ///// <param name="schemaUrl"></param>
        ///// <returns></returns>
        //public static Version GetVersion(JsonDocument json)
        //{
        //    Version? version = null;

        //    Log.Warning("Falling back to $schema for version number.");
        //    var schemaUrl = json.GetProperty<Uri>("$schema");
        //    if (schemaUrl != null)
        //    {

        //        var segments = schemaUrl.AbsolutePath.Split('/');
        //        if (segments.Length >= 2)
        //        {
        //            Version.TryParse(segments[segments.Length - 2], out version);
        //        }
        //    }

        //    return version ?? new Version(0, 0, 0);
        //}
    }
}
