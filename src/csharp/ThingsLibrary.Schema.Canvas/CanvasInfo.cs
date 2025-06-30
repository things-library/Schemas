// ================================================================================
// <copyright file="CanvasInfo.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasInfo
    {
        ///// <summary>
        ///// Section Id
        ///// </summary>
        //[ConfigurationKeyName("$id")]
        //[JsonPropertyName("$id")]
        //[Display(Name = "Section Id"), StringLength(200), Required]
        //public string Id => this.GetType().Name;

        /// <summary>
        /// Unique Namespace ID
        /// </summary>
        [StringLength(200, ErrorMessage = "ID must be between 3 and 200 characters", MinimumLength = 3), Required]
        public string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// Title
        /// </summary>        
        [Display(Name = "Service Name"), StringLength(100, MinimumLength = 1), Required]
        public string Name { get; init; } = string.Empty;
        
        /// <summary>
        /// Description
        /// </summary>
        [Display(Name = "Description"), StringLength(500, MinimumLength = 1), Required]
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// Application / Group Name 
        /// </summary>
        [Display(Name = "Service Group Name"), StringLength(100, MinimumLength = 1)]
        public string GroupName { get; init; } = string.Empty;

        /// <summary>
        /// Assembly / App Version
        /// </summary>
        [Display(Name = "Version")]
        public Version? Version { get; init; } = new Version();
        

        /// <summary>
        /// DNS Hostname
        /// </summary>
        [Display(Name = "Host (Uri)"), DataType(DA.DataType.Url)]
        public Uri? Host { get; init; }

        /// <summary>
        /// Route Prefix / PathBase
        /// </summary>
        [Display(Name = "Route Prefix"), DataType(DA.DataType.Url)]
        public string? RoutePrefix { get; init; }
        
        /// <summary>
        /// Environment Name
        /// </summary>
        [Display(Name = "Environment Name"), StringLength(100, MinimumLength = 1), Required]
        public string Environment { get; init; } = string.Empty;

        /// <summary>
        /// Debugging capabilities turned on, increased logging, and various details
        /// </summary>
        public bool DebugMode { get; set; } = false;

        /// <summary>
        /// Production Environment
        /// </summary>
        [JsonIgnore]
        public bool IsProduction => (string.Compare(this.Environment, "Production", true) == 0);

        /// <summary>
        /// Development Environment
        /// </summary>
        [JsonIgnore]
        public bool IsDevelopment => (string.Compare(this.Environment, "Development", true) == 0);
        
        /// <summary>
        /// Capabilities
        /// </summary>
        [Display(Name = "Capabilities", Description = "Comma delimited list of capabilities that this service provides."), StringLength(500)]
        public CanvasInfoCapabilities Capabilities { get; init; } = new CanvasInfoCapabilities();

        /// <summary>
        /// Capabilities
        /// </summary>
        [Display(Name = "Cors", Description = "CORS policies.")]
        public Dictionary<string, CanvasInfoCors> Cors { get; init; } = new Dictionary<string, CanvasInfoCors>();
          
        /// <summary>
        /// Open API Information
        /// </summary>
        [Display(Name = "Contact Information")]
        public CanvasInfoContact? Contact { get; init; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public CanvasInfo()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly() ?? throw new ArgumentException("Entry Assembly unknown.");

            this.Version = assembly.GetName().Version;
        }
    }
}
