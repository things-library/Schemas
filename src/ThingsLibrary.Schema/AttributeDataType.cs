namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Attribute Data Types
    /// </summary>
    /// <example>lookup, value, value_int, date, date_time, time, text, email, uri, boolean</example>
    public static class AttributeDataTypes
    {
        public const string Boolean = "boolean";
        public const string Currency = "currency";
        public const string Date = "date";
        public const string DateTime = "date_time";
        public const string Duration = "duration";
        public const string Email = "email";
        public const string Enum = "enum";
        public const string Html = "html";
        public const string Password = "password";
        public const string Phone = "phone";
        public const string String = "string";
        public const string TextArea = "text";
        public const string Time = "time";
        public const string Url = "url";
        public const string Value = "decimal";
        public const string ValueInt = "int";

        public const string ValueRange = "decimal_range";
        public const string ValueIntRange = "int_range";
        public const string CurrencyRange = "currency_range";

        /// <summary>
        /// Static list of attribute data types
        /// </summary>
        public static Dictionary<string, AttributeDataType> Items { get; } = GetAll().ToDictionary(x => x.Key, x => x);

        /// <summary>
        /// Returns the static listing of attribute data types
        /// </summary>
        /// <returns></returns>
        public static List<AttributeDataType> GetAll()
        {
            return new List<AttributeDataType>()
            {
                new() { Key = Boolean,    Name = "True / False",      Type = "boolean", InputType = "checkbox",      Format = ""         },
                new() { Key = Currency,   Name = "Currency",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = Date,       Name = "Date Only",         Type = "string",  InputType = "date",          Format = "date"     },
                new() { Key = DateTime,   Name = "Date + Time",       Type = "string",  InputType = "datetime",      Format = "date-time"  },
                new() { Key = Duration,   Name = "Duration",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = Email,      Name = "Email",             Type = "string",  InputType = "email",         Format = "email"    },
                new() { Key = Enum,       Name = "Pick List",         Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = Html,       Name = "HTML",              Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = Password,   Name = "Password",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = Phone,      Name = "Phone Number",      Type = "string",  InputType = "tel",           Format = ""         },
                new() { Key = String,     Name = "String",            Type = "string",  InputType = "text",          Format = ""       },   //DEFAULT ASSUMED
                new() { Key = TextArea,   Name = "Text (MultiLine)",  Type = "string",  InputType = "textarea",      Format = "textarea" },
                new() { Key = Time,       Name = "Time Only",         Type = "string",  InputType = "time",          Format = "time"     },
                new() { Key = Url,        Name = "Url",               Type = "string",  InputType = "url",           Format = "uri"      },
                new() { Key = Value,      Name = "Number (Decimals)", Type = "number",  InputType = "number",        Format = ""         },
                new() { Key = ValueInt,   Name = "Number (Whole)",    Type = "integer", InputType = "number",        Format = ""         },
                new() { Key = ValueRange, Name = "Number Range (Decimals)", Type = "number", InputType = "number",    Format = ""},
                new() { Key = ValueIntRange,  Name = "Number Range (Whole)", Type = "integer", InputType = "number",   Format = "" },
                new() { Key = CurrencyRange,  Name = "Currency Range", Type = "number", InputType = "number",   Format = "" }
            };
        }       
    }


    /// <summary>
    /// Attribute Data Type
    /// </summary>
    [DebuggerDisplay("{Name} ({InputType})")]
    public record AttributeDataType
    {
        /// <summary>
        /// Unique Key
        /// </summary>
        [Key, Required, Display(Name = "Key"), StringLength(50, MinimumLength = 2)]
        [RegularExpression("^[a-z0-9_]*$", ErrorMessage = "Invalid Characters.  Please only use lowercase letters, numeric, and underscores.")]
        public string Key { get; init; } = string.Empty;

        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Html Input Type
        /// </summary>
        [Display(Name = "Input Name"), StringLength(50), Required]
        public string InputType { get; init; } = string.Empty;

        /// <summary>
        /// JSON Schema 'type' field.. expected: string, number, integer, boolean
        /// </summary>
        [Display(Name = "Type"), StringLength(50), Required]
        public string Type { get; init; } = string.Empty;

        /// <summary>
        /// JSON Schema 'format' field.. expected: date, date-time, email, uri, text, time, null
        /// </summary>
        [Display(Name = "Format"), StringLength(50)]
        public string Format { get; init; } = string.Empty;
    }
}
