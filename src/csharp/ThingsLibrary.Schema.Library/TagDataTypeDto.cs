// ================================================================================
// <copyright file="TagDataTypeDto.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{    
    /// <summary>
    /// Tag Data Types
    /// </summary>
    /// <example>lookup, value, value_int, date, date_time, time, text, email, uri, boolean</example>
    public static class TagDataTypes
    {
        public const string Boolean = "boolean";
        public const string Currency = "currency";
        public const string CurrencyRange = "currency_range";
        public const string Date = "date";
        public const string DateTime = "date_time";
        public const string Decimal = "decimal";
        public const string DecimalRange = "decimal_range";
        public const string Duration = "duration";
        public const string Email = "email";
        public const string Enum = "enum";
        public const string Html = "html";
        public const string Integer = "int";
        public const string IntegerRange = "int_range";
        public const string Password = "password";
        public const string Phone = "phone";
        public const string String = "string";
        public const string TextArea = "text";
        public const string Time = "time";
        public const string Url = "url";
        
        /// <summary>
        /// Static list of tag data types
        /// </summary>
        public static Dictionary<string, TagDataTypeDto> Items { get; } = GetAll().ToDictionary(x => x.Key, x => x);
        
        /// <summary>
        /// Returns the static listing of tag data types
        /// </summary>
        /// <returns></returns>
        public static List<TagDataTypeDto> GetAll()
        {
            return new List<TagDataTypeDto>()
            {
                new() { Key = TagDataTypes.Boolean,    Name = "True / False",      Type = "boolean", InputType = "checkbox",      Format = ""         },
                new() { Key = TagDataTypes.Currency,   Name = "Currency",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = TagDataTypes.Date,       Name = "Date Only",         Type = "string",  InputType = "date",          Format = "date"     },
                new() { Key = TagDataTypes.DateTime,   Name = "Date + Time",       Type = "string",  InputType = "datetime",      Format = "date-time"  },
                new() { Key = TagDataTypes.Duration,   Name = "Duration",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = TagDataTypes.Email,      Name = "Email",             Type = "string",  InputType = "email",         Format = "email"    },
                new() { Key = TagDataTypes.Enum,       Name = "Pick List",         Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = TagDataTypes.Html,       Name = "HTML",              Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = TagDataTypes.Password,   Name = "Password",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = TagDataTypes.Phone,      Name = "Phone Number",      Type = "string",  InputType = "tel",           Format = ""         },
                new() { Key = TagDataTypes.String,     Name = "String",            Type = "string",  InputType = "text",          Format = ""       },   //DEFAULT ASSUMED (Safe)
                new() { Key = TagDataTypes.TextArea,   Name = "Text (MultiLine)",  Type = "string",  InputType = "textarea",      Format = "textarea" },
                new() { Key = TagDataTypes.Time,       Name = "Time Only",         Type = "string",  InputType = "time",          Format = "time"     },
                new() { Key = TagDataTypes.Url,        Name = "Url",               Type = "string",  InputType = "url",           Format = "uri"      },
                new() { Key = TagDataTypes.Decimal,    Name = "Number (Decimals)", Type = "number",  InputType = "number",        Format = ""         },
                new() { Key = TagDataTypes.Integer,    Name = "Number (Whole)",    Type = "integer", InputType = "number",        Format = ""         },
                new() { Key = TagDataTypes.DecimalRange, Name = "Number Range (Decimals)", Type = "number", InputType = "number",    Format = ""},
                new() { Key = TagDataTypes.IntegerRange,  Name = "Number Range (Whole)", Type = "integer", InputType = "number",   Format = "" },
                new() { Key = TagDataTypes.CurrencyRange,  Name = "Currency Range", Type = "number", InputType = "number",   Format = "" }
            };
        }

        /// <summary>
        /// See if we can figure out a tag data type based on the key
        /// </summary>
        /// <param name="tagKey"></param>
        /// <returns></returns>
        public static string GetDefault(string tagKey)
        {
            if (tagKey.EndsWith("_date")) { return TagDataTypes.Date; }     // *_date = date only

            if (tagKey.EndsWith("_price") ||
                tagKey.EndsWith("_cost") ||
                tagKey == "tax" ||
                tagKey == "total" ||
                tagKey == "subtotal")
            {
                return TagDataTypes.Currency;
            }

            if (tagKey.EndsWith("_url")) { return TagDataTypes.Url; }
            if (tagKey == "location") { return TagDataTypes.Enum; }
            if (tagKey == "time") { return TagDataTypes.Time; }
            if (tagKey == "email" || tagKey == "email_address") { return TagDataTypes.Email; }

            if (tagKey == "description" ||
                tagKey == "notes" ||
                tagKey == "details"
            )
            {
                return TagDataTypes.TextArea;
            }

            // default to text            
            return TagDataTypes.String;
        }

        /// <summary>
        /// Get Tag data type based on generic object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>TagDataType object based on type</returns>
        public static TagDataTypeDto GetType(Type type)
        {
            switch (type)
            {
                case Type t when t == typeof(string): { return TagDataTypes.Items[TagDataTypes.String]; }
                
                case Type t when t == typeof(int): { return TagDataTypes.Items[TagDataTypes.Integer]; }
                case Type t when t == typeof(decimal): { return TagDataTypes.Items[TagDataTypes.Decimal]; }
                case Type t when t == typeof(DateTime): { return TagDataTypes.Items[TagDataTypes.DateTime]; }
                case Type t when t == typeof(DateTimeOffset): { return TagDataTypes.Items[TagDataTypes.Date]; }
                case Type t when t == typeof(DateOnly): { return TagDataTypes.Items[TagDataTypes.Date]; }
                case Type t when t == typeof(TimeOnly): { return TagDataTypes.Items[TagDataTypes.Time]; }
                case Type t when t == typeof(Uri): { return TagDataTypes.Items[TagDataTypes.Url]; }
                case Type t when t == typeof(bool): { return TagDataTypes.Items[TagDataTypes.Boolean]; }
                case Type t when t == typeof(TimeSpan): { return TagDataTypes.Items[TagDataTypes.Duration]; }

                // Add more cases as needed
                default:
                    { 
                        return TagDataTypes.Items[TagDataTypes.String]; 
                    }
            }
        }
    }
    

    /// <summary>
    /// Tag Data Type
    /// </summary>
    [DebuggerDisplay("{Name} ({InputType})")]    
    public record TagDataTypeDto
    {
        /// <summary>
        /// Unique Key
        /// </summary>
        [Key, Required, Display(Name = "Key"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternErrorMessage)]
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


        public TagDataTypeDto()
        {
            //nothing
        }
    }
}
