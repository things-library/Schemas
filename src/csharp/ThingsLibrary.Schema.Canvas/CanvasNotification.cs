// ================================================================================
// <copyright file="CanvasNotification.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Canvas
{
    public class CanvasNotification
    {
        /// <summary>
        /// Email Notification Settings
        /// </summary>
        [Display(Name = "Email")]
        public CanvasNotificationEmail? Email { get; set; }

        /// <summary>
        /// SMS Notification Settings
        /// </summary>
        [Display(Name = "SMS")]
        public CanvasNotificationEmail? Sms { get; set; }



        public CanvasNotification()
        {
            //nothing
        }
    }
}

