// ================================================================================
// <copyright file="TelemetryEventType.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Telemetry
{
    /// <summary>
    /// Item type definition language
    /// </summary>    
    [DebuggerDisplay("{Name}")]
    public class TelemetryEventTypeDto
    {
        /// <summary>
        /// Item Type Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 1), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>
        [ValidateCollectionItems]
        [JsonPropertyName("tags"), JsonIgnoreEmptyCollection]
        public IDictionary<string, ItemTypeTagDto> Tags { get; set; } = new Dictionary<string, ItemTypeTagDto>();
                
        /// <summary>
        /// Generic metadata which is a simple key-value dictionary
        /// </summary>
        [JsonPropertyName("meta"), JsonIgnoreEmptyCollection]
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor
        /// </summary>
        public TelemetryEventTypeDto()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TelemetryEventTypeDto(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Easy lookup and empty string lookup
        /// </summary>
        /// <param name="key">Dictionary Key</param>
        /// <param name="isMeta">If the value from metadata</param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (!this.Tags.ContainsKey(key)) { return string.Empty; }

                return this.Tags[key].Name;
            }           
        }
    }
}
