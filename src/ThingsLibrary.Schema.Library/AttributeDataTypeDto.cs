// ================================================================================
// <copyright file="AttributeDataTypeDto.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{    
    /// <summary>
    /// Attribute Data Types
    /// </summary>
    /// <example>lookup, value, value_int, date, date_time, time, text, email, uri, boolean</example>
    public static class AttributeDataTypes
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
        /// Static list of attribute data types
        /// </summary>
        public static Dictionary<string, AttributeDataTypeDto> Items { get; } = GetAll().ToDictionary(x => x.Key, x => x);
        
        /// <summary>
        /// Returns the static listing of attribute data types
        /// </summary>
        /// <returns></returns>
        public static List<AttributeDataTypeDto> GetAll()
        {
            return new List<AttributeDataTypeDto>()
            {
                new() { Key = AttributeDataTypes.Boolean,    Name = "True / False",      Type = "boolean", InputType = "checkbox",      Format = ""         },
                new() { Key = AttributeDataTypes.Currency,   Name = "Currency",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = AttributeDataTypes.Date,       Name = "Date Only",         Type = "string",  InputType = "date",          Format = "date"     },
                new() { Key = AttributeDataTypes.DateTime,   Name = "Date + Time",       Type = "string",  InputType = "datetime",      Format = "date-time"  },
                new() { Key = AttributeDataTypes.Duration,   Name = "Duration",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = AttributeDataTypes.Email,      Name = "Email",             Type = "string",  InputType = "email",         Format = "email"    },
                new() { Key = AttributeDataTypes.Enum,       Name = "Pick List",         Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = AttributeDataTypes.Html,       Name = "HTML",              Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = AttributeDataTypes.Password,   Name = "Password",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = AttributeDataTypes.Phone,      Name = "Phone Number",      Type = "string",  InputType = "tel",           Format = ""         },
                new() { Key = AttributeDataTypes.String,     Name = "String",            Type = "string",  InputType = "text",          Format = ""       },   //DEFAULT ASSUMED (Safe)
                new() { Key = AttributeDataTypes.TextArea,   Name = "Text (MultiLine)",  Type = "string",  InputType = "textarea",      Format = "textarea" },
                new() { Key = AttributeDataTypes.Time,       Name = "Time Only",         Type = "string",  InputType = "time",          Format = "time"     },
                new() { Key = AttributeDataTypes.Url,        Name = "Url",               Type = "string",  InputType = "url",           Format = "uri"      },
                new() { Key = AttributeDataTypes.Decimal,    Name = "Number (Decimals)", Type = "number",  InputType = "number",        Format = ""         },
                new() { Key = AttributeDataTypes.Integer,    Name = "Number (Whole)",    Type = "integer", InputType = "number",        Format = ""         },
                new() { Key = AttributeDataTypes.DecimalRange, Name = "Number Range (Decimals)", Type = "number", InputType = "number",    Format = ""},
                new() { Key = AttributeDataTypes.IntegerRange,  Name = "Number Range (Whole)", Type = "integer", InputType = "number",   Format = "" },
                new() { Key = AttributeDataTypes.CurrencyRange,  Name = "Currency Range", Type = "number", InputType = "number",   Format = "" }
            };
        }

        /// <summary>
        /// See if we can figure out a attribute data type based on the key
        /// </summary>
        /// <param name="attributeKey"></param>
        /// <returns></returns>
        public static string GetDefault(string attributeKey)
        {
            if (attributeKey.EndsWith("_date")) { return AttributeDataTypes.Date; }     // *_date = date only

            if (attributeKey.EndsWith("_price") ||
                attributeKey.EndsWith("_cost") ||
                attributeKey == "tax" ||
                attributeKey == "total" ||
                attributeKey == "subtotal")
            {
                return AttributeDataTypes.Currency;
            }

            if (attributeKey.EndsWith("_url")) { return AttributeDataTypes.Url; }
            if (attributeKey == "location") { return AttributeDataTypes.Enum; }
            if (attributeKey == "time") { return AttributeDataTypes.Time; }
            if (attributeKey == "email" || attributeKey == "email_address") { return AttributeDataTypes.Email; }

            if (attributeKey == "description" ||
                attributeKey == "notes" ||
                attributeKey == "details"
            )
            {
                return AttributeDataTypes.TextArea;
            }

            // default to text            
            return AttributeDataTypes.String;
        }

        /// <summary>
        /// Get Attribute data type based on generic object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>AttributeDataType object based on type</returns>
        public static AttributeDataTypeDto GetType(Type type)
        {
            switch (type)
            {
                case Type t when t == typeof(string): { return AttributeDataTypes.Items[AttributeDataTypes.String]; }
                
                case Type t when t == typeof(int): { return AttributeDataTypes.Items[AttributeDataTypes.Integer]; }
                case Type t when t == typeof(decimal): { return AttributeDataTypes.Items[AttributeDataTypes.Decimal]; }
                case Type t when t == typeof(DateTime): { return AttributeDataTypes.Items[AttributeDataTypes.DateTime]; }
                case Type t when t == typeof(DateTimeOffset): { return AttributeDataTypes.Items[AttributeDataTypes.Date]; }
                case Type t when t == typeof(DateOnly): { return AttributeDataTypes.Items[AttributeDataTypes.Date]; }
                case Type t when t == typeof(TimeOnly): { return AttributeDataTypes.Items[AttributeDataTypes.Time]; }
                case Type t when t == typeof(Uri): { return AttributeDataTypes.Items[AttributeDataTypes.Url]; }
                case Type t when t == typeof(bool): { return AttributeDataTypes.Items[AttributeDataTypes.Boolean]; }
                case Type t when t == typeof(TimeSpan): { return AttributeDataTypes.Items[AttributeDataTypes.Duration]; }

                // Add more cases as needed
                default:
                    { 
                        return AttributeDataTypes.Items[AttributeDataTypes.String]; 
                    }
            }
        }
    }
    

    /// <summary>
    /// Attribute Data Type
    /// </summary>
    [DebuggerDisplay("{Name} ({InputType})")]    
    public record AttributeDataTypeDto
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


        public AttributeDataTypeDto()
        {
            //nothing
        }
    }
}
