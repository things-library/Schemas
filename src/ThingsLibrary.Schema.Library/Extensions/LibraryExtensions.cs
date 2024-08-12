using System.Globalization;
using System.Text;

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class LibraryExtensions
    {
        public static char[] SeperatorCharacters = " \\/-_[]{}.".ToCharArray();
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

            //nothing to do?
            if (string.IsNullOrEmpty(text)) { return string.Empty; }    
            if (Base.SchemaBase.IsKeyValid(text)) { return text; }
            
            var sb = new StringBuilder();

            // only append the first character if it matches our regex
            char c = char.ToLowerInvariant(text[0]);            
            if (!SeperatorCharacters.Contains(c))
            {   
                sb.Append(c);
            }            
            if (text.Length < 2) { return sb.ToString(); }
            
            // now look at all the other characters
            for (int i = 1; i < text.Length; ++i)
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
                else if (char.IsUpper(c))
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] != '_') { sb.Append('_'); }
                    sb.Append(char.ToLowerInvariant(c));
                }
                else if(Char.IsAsciiLetter(c))
                {                                        
                    sb.Append(char.ToLowerInvariant(c));                    
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

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(key.ToLower().Replace('_', ' '));
        }
    }
}
