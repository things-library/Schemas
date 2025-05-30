// ================================================================================
// <copyright file="Checksum.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class ChecksumExtensions
    {
        /// <summary>
        /// Append the checksum
        /// </summary>
        /// <param name="sentence"></param>
        /// <remarks>Checksum based on the NMEA0183 checksum which is just a simple byte-by-byte XOR of all the bytes between $ and * signs exclusively.</remarks>
        public static void AppendChecksum(this StringBuilder sentence)
        {
            // not the beginning of a sentence
            if (sentence.Length == 0) { return; }
            if (sentence[0] != '$') { return; }

            //Start with first Item
            int checksum = Convert.ToByte(sentence[1]);

            // Loop through all chars to get a checksum
            int i;
            for (i = 2; i < sentence.Length; i++)
            {
                if (sentence[i] == '*') { break; }

                // No. XOR the checksum with this character's value
                checksum ^= Convert.ToByte(sentence[i]);
            }

            // no astrisk to mark the CRC check
            if (i == sentence.Length)
            {
                sentence.Append("*");
            }

            // Return the checksum formatted as a two-character hexadecimal
            sentence.Append(checksum.ToString("X2"));
        }

        /// <summary>
        /// Calculate a checksum value based on the NMEA0183 style calculation
        /// </summary>
        /// <param name="sentence">Sentence</param>
        /// <remarks>Checksum based on the NMEA0183 checksum which is just a simple byte-by-byte XOR of all the bytes between $ and * signs exclusively.</remarks>
        /// <returns>Two character hexadecimal checksum value</returns>
        public static string ToChecksum(this string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) { return string.Empty; }

            // not the beginning of a sentence
            if (sentence[0] != '$') { return string.Empty; }

            //Start with first Item
            int checksum = Convert.ToByte(sentence[1]);

            // Loop through all chars to get a checksum
            for (int i = 2; i < sentence.Length; i++)
            {
                if (sentence[i] == '*') { break; }

                // No. XOR the checksum with this character's value
                checksum ^= Convert.ToByte(sentence[i]);
            }

            // Return the checksum formatted as a two-character hexadecimal
            return checksum.ToString("X2");
        }

        /// <summary>
        /// Validate that the checksum on the sentence is correct
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static bool ValidateChecksum(this string sentence)
        {
            var checksum = sentence.ToChecksum();
            if (string.IsNullOrEmpty(checksum)) { return false; }

            return sentence.EndsWith(checksum);
        }

        /// <summary>
        /// Convert a telemetry string into a Item object
        /// </summary>
        /// <param name="telemetrySentence"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ItemDto ToItem(this string telemetrySentence)
        {
            //  $1724387849602|PA|r:1|s:143|p:PPE Mask|q:1|p:000*79
            //  $1724387850520|ET|r:1|q:2*33
            //  $PA|r:1|s:143|p:PPE Mask|q:1|p:000*79

            ArgumentNullException.ThrowIfNullOrEmpty(telemetrySentence);
            if (!telemetrySentence.ValidateChecksum()) { throw new ArgumentException("Invalid checksum"); }
            if (telemetrySentence[0] != '$') { throw new ArgumentException("Invalid telemetry sentence"); }

            // create item

            var pos = telemetrySentence.LastIndexOf('*');
            if (pos < 0) { throw new ArgumentException("Unable to find end of telemetry sentence"); }

            var parts = telemetrySentence.Substring(1, pos - 1).Split('|');

            int i = 0;

            // TIMESTAMP
            DateTimeOffset? timestamp = null;
            if (parts[i].Length == 13)
            {
                timestamp = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(parts[i]));
                i++;    //move to next field
            }

            var item = new ItemDto()
            {
                Date = timestamp,
                Type = parts[i]     // SENTENCE ID
            };

            // ATTRIBUTE TAGS
            for (i = i + 1; i < parts.Length; i++)
            {
                pos = parts[i].IndexOf(':');
                if (pos < 0) { continue; }   // BAD PAIRING?

                item.Add(parts[i].Substring(0, pos), parts[i].Substring(pos + 1), false);
            }

            return item;
        }

        /// <summary>
        /// Convert a telemetry item (aka: with date) to a telemetry sentence
        /// </summary>
        /// <param name="telemetryItem">Telemetry Item</param>
        /// <returns></returns>
        public static string ToTelemetrySentence(this ItemDto telemetryItem)
        {
            ArgumentNullException.ThrowIfNull(telemetryItem);
            ArgumentNullException.ThrowIfNull(telemetryItem.Date);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(telemetryItem.Type);

            //EXAMPLES:
            //  $1724387849602|PA|r:1|s:143|p:PPE Mask|q:1|p:000*79
            //  $1724387850520|ET|r:1|q:2*33
            //  $PA|r:1|s:143|p:PPE Mask|q:1|p:000*79

            var sentence = new StringBuilder();

            // start 
            sentence.Append('$');

            // date (Epoch MS)
            sentence.Append(telemetryItem.Date.Value.ToUnixTimeMilliseconds());

            // sentence ID
            sentence.Append('|');
            sentence.Append(telemetryItem.Type);

            foreach (var attribute in telemetryItem.Attributes)
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
