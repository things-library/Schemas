// ================================================================================
// <copyright file="SchemaBase.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ThingsLibrary.Schema.Base
{
    /// <summary>
    /// Base Schema Attributes
    /// </summary>
    public static class SchemaBase
    {
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


        
        private readonly static char[] SeperatorCharacters = " \\/-_[]{}.".ToCharArray();

        /// <summary>
        /// Converts text to snake_case key
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToKey(this string text)
        {
            // EXAMPLES:
            //  "test_Key"
            //  "TestKey"
            //  "Test Key"
            //  "test key"
            //  "test.Key"
            //  "TESTKEY"
            //  "TEST KEY"
            //  "testKey"
            //  "TestKeY"
            //  "Test52"
            //  "Test52Something"

            //nothing to do?
            if (string.IsNullOrEmpty(text)) { return string.Empty; }

            // Check for a case of 'mixed' case indicating possible seperation of words using pascalCase or SententCase 
            if (!(text.Any(x => char.IsLower(x)) && text.Any(x => char.IsUpper(x))))
            {
                text = text.ToLower();
            }

            //nothing to do?            
            if (Base.SchemaBase.IsKeyValid(text)) { return text; }

            var sb = new StringBuilder();

            // only append the first character if it matches our regex
            char c;

            // now look at all the other characters
            for (int i = 0; i < text.Length; ++i)
            {
                c = text[i];
                if (SeperatorCharacters.Contains(c))
                {
                    // we don't want multiple seperator characters back to back
                    if (sb.Length > 0 && sb[sb.Length - 1] != '_')
                    {
                        sb.Append('_');
                    }
                }
                else if (i > 0 && char.IsUpper(c))
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] != '_') { sb.Append('_'); }
                    sb.Append(char.ToLowerInvariant(c));
                }
                else if (char.IsAsciiLetter(c))
                {
                    sb.Append(char.ToLowerInvariant(c));
                }
                else if (char.IsNumber(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a snake case key value to title casing
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static string ToDisplayName(this string key)
        {
            // replace the _ with space so that title case finds all the words

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(key.ToLower().Replace('_', ' ').Replace('-', ' '));
        }

        /// <summary>
        /// Clone the object using serialization and deserialization
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="obj">Object to clone</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Clone<T>(this T obj) where T : class
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj)) ?? throw new ArgumentException("Unable to clone item.");
        }
    }
}
