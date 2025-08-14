// ================================================================================
// <copyright file="CanvasNotificationSms.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasNotificationSms
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;


        /// <summary>
        /// From Address for notifications being sent.
        /// </summary>
        [Display(Name = "From Address"), StringLength(200), Required, DataType(DT.PhoneNumber)]
        public string AddressFrom { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;



        public CanvasNotificationSms()
        {
            //nothing
        }
    }
}

