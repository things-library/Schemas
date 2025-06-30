// ================================================================================
// <copyright file="TelemItem.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Telemetry
{
    /// <summary>
    /// Item Schema - Flexible
    /// </summary>
    [DebuggerDisplay("Date: {Date}, Type: {Type})")]
    public class TelemItemDto
    {           
        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset Date { get; set; }
                
        /// <summary>
        /// Item Type - describes what type of item we are talking about.
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 1), Required]        
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("tags"), JsonIgnoreEmptyCollection]
        [Display(Name = "Tags")]
        public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public TelemItemDto()
        {
            //nothing
        }

        public override string ToString() => this.ToTelemetrySentence();
    }
}
