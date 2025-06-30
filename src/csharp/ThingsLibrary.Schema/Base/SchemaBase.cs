// ================================================================================
// <copyright file="SchemaBase.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;
using System.Text.RegularExpressions;

namespace ThingsLibrary.Schema.Base
{
    /// <summary>
    /// Base Schema Attributes
    /// </summary>
    public static class SchemaBase
    {
        #region --- Static ---

        // Should be sent with mime type: "application/schema+json"

        /// <summary>
        /// Version string
        /// </summary>
        public const string CurrentVersion = "1.1";

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

        #region --- Keys ---

        public static string GenerateKey()
        {
            return ToShortCode(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        }

        public static string GenerateKey(string value)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            string pattern = "^[a-z0-9_]+$";
            var regex = new Regex(pattern);

            value = value
                .ToLower()
                .Replace("@", ".")
                .Replace("`", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("%", "_pct_");

            var key = new StringBuilder(value.Length);
            foreach(var c in value)
            {
                key.Append((regex.IsMatch(c.ToString()) ? c : '_'));
            }
            
            // split it based on the underscore and remove any empty entries aka: __ or ___ or ____
            return string.Join('_', key.ToString().Split('_', StringSplitOptions.RemoveEmptyEntries));
        }
                
        public static bool IsKeyValid(string key)
        {
            return IsKeyValid(key, SchemaBase.KeyPattern);
        }

        public static bool IsKeyValid(string key, string keyPattern)
        {
            return Regex.IsMatch(key, keyPattern);
        }


        /// <summary>
        /// Generate a short version of the long number based on the provided key space
        /// </summary>
        /// <param name="number">Base Number</param>
        /// <param name="keyspace">Characters to use in encoding</param>
        /// <returns></returns>
        /// <remarks>This is the same extension found in ThingsLibrary.DataType.Extensions</remarks>
        private static string ToShortCode(long number, string keyspace = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            if (number == 0) { return string.Empty; }

            var keyspaceLength = keyspace.Length;
            var numberToEncode = number;
            StringBuilder result = new StringBuilder();

            var i = 0;
            do
            {
                i++;
                var characterValue = (numberToEncode % keyspaceLength == 0 ? keyspaceLength : numberToEncode % keyspaceLength);
                var indexer = (int)characterValue - 1;

                // add to beginning
                result.Insert(0, keyspace[indexer]);

                numberToEncode = ((numberToEncode - characterValue) / keyspaceLength);
            }
            while (numberToEncode != 0);

            return result.ToString();
        }

        #endregion

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

        public static void ConfigureJsonSerializerOptions(JsonSerializerOptions options)
        {
            options.AllowTrailingCommas = JsonSerializerOptions.AllowTrailingCommas;
            options.WriteIndented = JsonSerializerOptions.WriteIndented;
            options.DefaultIgnoreCondition = JsonSerializerOptions.DefaultIgnoreCondition;
            options.PropertyNamingPolicy = JsonSerializerOptions.PropertyNamingPolicy;
            options.PropertyNameCaseInsensitive = JsonSerializerOptions.PropertyNameCaseInsensitive;
            options.TypeInfoResolver = JsonSerializerOptions.TypeInfoResolver;
        }

        #endregion
    }
}
