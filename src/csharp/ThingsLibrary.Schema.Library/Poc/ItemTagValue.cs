// ================================================================================
// <copyright file="ItemTagValue.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Poc
{
    /// <summary>
    /// Tags
    /// </summary>
    [DebuggerDisplay("{Value})")]
    public class ItemTagValueDto
    {
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        [Display(Name = "Value"), Required]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTagValueDto()
        {
            //nothing
        }

        public override string ToString() => this.Value;        
    }
}
