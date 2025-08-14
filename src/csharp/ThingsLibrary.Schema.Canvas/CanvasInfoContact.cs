// ================================================================================
// <copyright file="CanvasInfoContact.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{ 
    /// <summary>
    /// Contact Card
    /// </summary>    
    public class CanvasInfoContact
    {
        /// "Contact": 
        /// {
        ///     "Name": "Mark Lanning",
        ///     "Email": "mark.lanning@test.org",
        ///     "Company": "Symphony",
        ///     "Department": "ITO",
        ///     "ProjectUri": "https://ado.devops.com/something/_git/BaseServiceBFF"
        /// }

        /// <summary>
        /// Contact Name
        /// </summary>
        [Display(Name = "Contact Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Contact Email Address
        /// </summary>
        [Display(Name = "Email"), StringLength(200), DataType(DT.EmailAddress), Required]
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Contact Company
        /// </summary>
        [Display(Name = "Company"), StringLength(200), Required]
        public string Company { get; init; } = string.Empty;

        /// <summary>
        /// Contact Department
        /// </summary>
        [Display(Name = "Department"), StringLength(200), Required]
        public string Department { get; init; } = string.Empty;

        /// <summary>
        /// Project Url
        /// </summary>
        [Display(Name = "Project Url"), StringLength(200), DataType(DT.Url)]
        public string ProjectUrl { get; init; } = string.Empty;
    }
}
