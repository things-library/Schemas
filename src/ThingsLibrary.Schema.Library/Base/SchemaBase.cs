using System.Text.RegularExpressions;

namespace ThingsLibrary.Schema.Library.Base
{
    /// <summary>
    /// Base Schema Attributes
    /// </summary>
    public class SchemaBase
    {
        #region --- Static ---

        // Should be sent with mime type: "application/schema+json"

        /// <summary>
        /// Version string
        /// </summary>
        public const string CurrentVersion = "1.0";

        /// <summary>
        /// Current Version
        /// </summary>
        /// <remarks>
        /// Semantic Versioning (https://semver.org/)
        /// MAJOR.MINOR.PATCH.BUILD
        /// - MAJOR is incremented when you make incompatible API changes
        /// - MINOR is incremented when you add functionality in a backwards-compatible manner
        /// - PATCH is incremented when you make backwards-compatible bug fixes
        /// </remarks> 
        public static Version SchemaVersion { get; } = new Version(CurrentVersion);     //typeof(SchemaBase).Assembly.GetName().Version;

        /// <summary>
        /// Json Schema Definition
        /// </summary>
        public static string SchemaBaseUrl { get; } = $"https://schema.thingslibrary.io/{CurrentVersion}";

        /// <summary>
        /// Pattern to use for all non-root library keys
        /// </summary>
        public const string KeyPattern = "^[a-z0-9_-]{1,50}$";

        /// <summary>
        /// Key pattern description
        /// </summary>
        public const string KeyPatternErrorMessage = "Invalid Characters.  Please only use lowercase letters, numeric, underscores and hyphens.";



        public static bool IsKeyValid(string key)
        {
            return IsKeyValid(key, SchemaBase.KeyPattern);
        }

        public static bool IsKeyValid(string key, string keyPattern)
        {
            return Regex.IsMatch(key, keyPattern);
        }

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
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("metadata"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Revision Number (1 = first, 0 = unknown/unspecified)
        /// </summary>
        [JsonPropertyName("version"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DefaultValue(0), Range(0, int.MaxValue)]
        public int Version { get; set; } = 0;   //0 = unknown/not specified


        /// <summary>
        /// Record ID
        /// </summary>
        /// <remarks>Not Serialized</remarks>
        [JsonIgnore]
        public Guid? Id { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        /// <remarks>Not Serialized</remarks>
        [JsonIgnore]
        public object? Tag { get; set; }
    }
}
