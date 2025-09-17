// ================================================================================
// <copyright file="ItemTagDataType.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library
{    
    /// <summary>
    /// Tag Data Types
    /// </summary>
    /// <example>lookup, value, value_int, date, date_time, time, text, email, uri, boolean</example>
    public static class ItemTagDataTypesDto
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
        public static Dictionary<string, ItemTagDataTypeDto> Items { get; } = GetAll().ToDictionary(x => x.Key, x => x);
        
        /// <summary>
        /// Returns the static listing of tag data types
        /// </summary>
        /// <returns></returns>
        public static List<ItemTagDataTypeDto> GetAll()
        {
            return new List<ItemTagDataTypeDto>()
            {
                new() { Key = ItemTagDataTypesDto.Boolean,    Name = "True / False",      Type = "boolean", InputType = "checkbox",      Format = ""         },
                new() { Key = ItemTagDataTypesDto.Currency,   Name = "Currency",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = ItemTagDataTypesDto.Date,       Name = "Date Only",         Type = "string",  InputType = "date",          Format = "date"     },
                new() { Key = ItemTagDataTypesDto.DateTime,   Name = "Date + Time",       Type = "string",  InputType = "datetime",      Format = "date-time"  },
                new() { Key = ItemTagDataTypesDto.Duration,   Name = "Duration",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = ItemTagDataTypesDto.Email,      Name = "Email",             Type = "string",  InputType = "email",         Format = "email"    },                
                new() { Key = ItemTagDataTypesDto.Html,       Name = "HTML",              Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = ItemTagDataTypesDto.Password,   Name = "Password",          Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = ItemTagDataTypesDto.Phone,      Name = "Phone Number",      Type = "string",  InputType = "tel",           Format = ""         },
                new() { Key = ItemTagDataTypesDto.Enum,       Name = "Pick List",         Type = "string",  InputType = "text",          Format = ""         },
                new() { Key = ItemTagDataTypesDto.String,     Name = "String",            Type = "string",  InputType = "text",          Format = ""       },   //DEFAULT ASSUMED (Safe)
                new() { Key = ItemTagDataTypesDto.TextArea,   Name = "Text (MultiLine)",  Type = "string",  InputType = "textarea",      Format = "textarea" },
                new() { Key = ItemTagDataTypesDto.Time,       Name = "Time Only",         Type = "string",  InputType = "time",          Format = "time"     },
                new() { Key = ItemTagDataTypesDto.Url,        Name = "Url",               Type = "string",  InputType = "url",           Format = "uri"      },
                new() { Key = ItemTagDataTypesDto.Decimal,    Name = "Decimal Number", Type = "number",  InputType = "number",        Format = ""         },
                new() { Key = ItemTagDataTypesDto.Integer,    Name = "Whole Number",    Type = "integer", InputType = "number",        Format = ""         },
                new() { Key = ItemTagDataTypesDto.DecimalRange, Name = "Decimal Range", Type = "number", InputType = "number",    Format = ""},
                new() { Key = ItemTagDataTypesDto.IntegerRange,  Name = "Whole Number Range", Type = "integer", InputType = "number",   Format = "" },
                new() { Key = ItemTagDataTypesDto.CurrencyRange,  Name = "Currency Range", Type = "number", InputType = "number",   Format = "" }
            };
        }

        /// <summary>
        /// See if we can figure out a tag data type based on the key
        /// </summary>
        /// <param name="tagKey"></param>
        /// <returns></returns>
        public static string GetDefault(string tagKey)
        {
            if (tagKey.EndsWith("_date")) { return ItemTagDataTypesDto.Date; }     // *_date = date only

            if (tagKey.EndsWith("_price") ||
                tagKey.EndsWith("_cost") ||
                tagKey == "tax" ||
                tagKey == "total" ||
                tagKey == "subtotal")
            {
                return ItemTagDataTypesDto.Currency;
            }

            if (tagKey.EndsWith("_url")) { return ItemTagDataTypesDto.Url; }
            if (tagKey == "location") { return ItemTagDataTypesDto.Enum; }
            if (tagKey == "time") { return ItemTagDataTypesDto.Time; }
            if (tagKey == "email" || tagKey == "email_address") { return ItemTagDataTypesDto.Email; }

            if (tagKey == "description" ||
                tagKey == "notes" ||
                tagKey == "details"
            )
            {
                return ItemTagDataTypesDto.TextArea;
            }

            // default to text            
            return ItemTagDataTypesDto.String;
        }

        public static bool IsValid(string typeKey, string value)
        {
            if (string.IsNullOrEmpty(value)) { return true; }   //empty means we want to remove the tag

            switch (typeKey)
            {
                case ItemTagDataTypesDto.Boolean:
                    {
                        return bool.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Currency:
                    {
                        // Accepts decimal with optional currency symbol, but here just check decimal
                        return decimal.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Date:
                    {
                        return DateOnly.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.DateTime:
                    {
                        return System.DateTime.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Duration:
                    {
                        // Accepts TimeSpan format (e.g., "01:30:00")
                        return TimeSpan.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Email:
                    {
                        // Simple email validation
                        try
                        {
                            var addr = new System.Net.Mail.MailAddress(value);
                            return addr.Address == value;
                        }
                        catch
                        {
                            return false;
                        }
                    }

                case ItemTagDataTypesDto.Enum: // pick-list
                    {                        
                        return true;
                    }

                case ItemTagDataTypesDto.Html:
                    {                        
                        return true;
                    }

                case ItemTagDataTypesDto.Password:
                    {                        
                        return true;
                    }

                case ItemTagDataTypesDto.Phone:
                    {
                        // Simple phone validation: digits, spaces, dashes, parentheses, plus
                        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[\d\s\-\+\(\)]+$");
                    }

                case ItemTagDataTypesDto.String:
                    {
                        return true;
                    }

                case ItemTagDataTypesDto.TextArea:
                    {
                        return true;
                    }

                case ItemTagDataTypesDto.Time:
                    {
                        return TimeOnly.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Url:
                    {
                        return Uri.TryCreate(value, UriKind.Absolute, out var uriResult)
                               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    }

                case ItemTagDataTypesDto.Decimal:
                    {
                        return decimal.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.Integer:
                    {
                        return int.TryParse(value, out _);
                    }

                case ItemTagDataTypesDto.DecimalRange:
                    {
                        // Accepts two decimals separated by a comma or dash
                        var decimalParts = value?.Split(new[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        return decimalParts?.Length == 2
                            && decimal.TryParse(decimalParts[0], out _)
                            && decimal.TryParse(decimalParts[1], out _);
                    }

                case ItemTagDataTypesDto.IntegerRange:
                    {
                        // Accepts two integers separated by a comma or dash
                        var intParts = value?.Split(new[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        return intParts?.Length == 2
                            && int.TryParse(intParts[0], out _)
                            && int.TryParse(intParts[1], out _);
                    }

                case ItemTagDataTypesDto.CurrencyRange:
                    {
                        // Accepts two decimals separated by a comma or dash
                        var currencyParts = value?.Split(new[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        return currencyParts?.Length == 2
                            && decimal.TryParse(currencyParts[0], out _)
                            && decimal.TryParse(currencyParts[1], out _);
                    }

                default:
                    {
                        return false;
                    }
            }            
        }

        /// <summary>
        /// Get Tag data type based on generic object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>TagDataType object based on type</returns>
        public static ItemTagDataTypeDto GetType(Type type)
        {
            switch (type)
            {
                case Type t when t == typeof(string): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.String]; }
                
                case Type t when t == typeof(int): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Integer]; }
                case Type t when t == typeof(decimal): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Decimal]; }
                case Type t when t == typeof(DateTime): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.DateTime]; }
                case Type t when t == typeof(DateTimeOffset): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Date]; }
                case Type t when t == typeof(DateOnly): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Date]; }
                case Type t when t == typeof(TimeOnly): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Time]; }
                case Type t when t == typeof(Uri): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Url]; }
                case Type t when t == typeof(bool): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Boolean]; }
                case Type t when t == typeof(TimeSpan): { return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.Duration]; }

                // Add more cases as needed
                default:
                    { 
                        return ItemTagDataTypesDto.Items[ItemTagDataTypesDto.String]; 
                    }
            }
        }
    }
    

    /// <summary>
    /// Tag Data Type
    /// </summary>
    [DebuggerDisplay("{Name} ({InputType})")]    
    public record ItemTagDataTypeDto
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
        [Display(Name = "Type"), StringLength(50, MinimumLength = 1), Required]
        public string Type { get; init; } = string.Empty;

        /// <summary>
        /// JSON Schema 'format' field.. expected: date, date-time, email, uri, text, time, null
        /// </summary>
        [Display(Name = "Format"), StringLength(50)]
        public string Format { get; init; } = string.Empty;


        public ItemTagDataTypeDto()
        {
            //nothing
        }
    }
}
