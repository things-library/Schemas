﻿// ================================================================================
// <copyright file="TelemetryItem.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;

namespace ThingsLibrary.Schema.Library.Telemetry.Extensions
{
    public static class TelemExtensions
    {
        /// <summary>
        /// Convert a telemetry string into a Item object
        /// </summary>
        /// <param name="telemetrySentence"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static TelemetryItemDto ToTelemetryItem(this string telemetrySentence)
        {
            //  $1724387849602|PA|r:1|s:143|p:PPE Mask|q:1|p:000*79
            //  $1724387850520|ET|r:1|q:2*33
            //  $PA|r:1|s:143|p:PPE Mask|q:1|p:000*79

            ArgumentException.ThrowIfNullOrEmpty(telemetrySentence);
            
            if (telemetrySentence[0] != '$' || !telemetrySentence.Contains("*")) { throw new ArgumentException("Invalid telemetry sentence"); }
            if (!telemetrySentence.ValidateChecksum()) { throw new ArgumentException("Invalid checksum"); }
            
            // create item
            var pos = telemetrySentence.LastIndexOf('*');
            if (pos < 0) { throw new ArgumentException("Unable to find end of telemetry sentence"); }

            var parts = telemetrySentence.Substring(1, pos - 1).Split('|');

            int i = 0;

            // TIMESTAMP
            DateTimeOffset timestamp;
            if (parts[i].Length == 13)
            {
                timestamp = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(parts[i]));
                i++;    //move to next field
            }
            else
            {
                timestamp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(parts[i]));
                i++;    //move to next field
            }

            var item = new TelemetryItemDto()
            {
                Date = timestamp,
                Type = parts[i]     // SENTENCE ID
            };

            // ATTRIBUTE TAGS
            for (i = i + 1; i < parts.Length; i++)
            {
                pos = parts[i].IndexOf(':');
                if (pos < 0) { continue; }   // BAD PAIRING?

                item.Tags.Add(parts[i].Substring(0, pos), parts[i].Substring(pos + 1));
            }

            return item;
        }

        /// <summary>
        /// Convert a telemetry item (aka: with date) to a telemetry sentence
        /// </summary>
        /// <param name="telemetryItem">Telemetry Item</param>
        /// <returns></returns>
        public static string ToTelemetrySentence(this TelemetryItemDto telemetryItem)
        {
            ArgumentNullException.ThrowIfNull(telemetryItem);
            //ArgumentNullException.ThrowIfNull(telemetryItem.Date);
            ArgumentException.ThrowIfNullOrWhiteSpace(telemetryItem.Type);

            //EXAMPLES:
            //  $1724387849602|PA|r:1|s:143|p:PPE Mask|q:1|p:000*79
            //  $1724387850520|ET|r:1|q:2*33
            //  $PA|r:1|s:143|p:PPE Mask|q:1|p:000*79

            var sentence = new StringBuilder();

            // start 
            sentence.Append('$');

            // date (Epoch MS)
            sentence.Append(telemetryItem.Date.ToUnixTimeMilliseconds());

            // sentence ID
            sentence.Append('|');
            sentence.Append(telemetryItem.Type);

            foreach (var attribute in telemetryItem.Tags)
            {
                sentence.Append('|');
                sentence.Append(attribute.Key);
                sentence.Append(':');
                sentence.Append(attribute.Value);
            }

            //Add checksum
            sentence.AppendChecksum();

            return sentence.ToString();
        }
    }
}
