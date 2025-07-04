// ================================================================================
// <copyright file="ExtensionTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;
using ThingsLibrary.Schema.Library.Telemetry;

namespace ThingsLibrary.Schema.Library.Tests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtensionTests
    {
        [TestMethod]
        [DataRow("test_key", "test_key")]   //already valid
        [DataRow("000_000", "000_000")]     //already valid
        [DataRow("test-key", "test-key")]   //already valid    
        [DataRow("000-000", "000-000")]     //already valid
        [DataRow("Test Key", "test_key")]
        [DataRow("TestKey", "test_key")]
        [DataRow("testKey", "test_key")]
        [DataRow("Test/Key", "test_key")]
        [DataRow("Test/key", "test_key")]
        [DataRow("/Test/key", "test_key")]
        [DataRow("Test.Key", "test_key")]
        [DataRow("Test.key", "test_key")]
        [DataRow(".test.key", "test_key")]
        [DataRow(".test././\\key", "test_key")]        
        [DataRow("TESTKEY", "testkey")]
        [DataRow("TEST KEY", "test_key")]
        [DataRow("tEsT_kEy", "t_es_t_k_ey")]
        [DataRow("Test52", "test52")]
        [DataRow("Test52Sen", "test52_sen")]
        [DataRow("Test52sen", "test52sen")]
        public void ToKey(string input, string expected)
        {
            var result = input.ToKey();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Telemetry()
        {
            var sentence = "$1724380000000|PA|r:1|s:143|p:PPE Mask|q:1|pr:414*44";

            var item = sentence.ToItem();

            // TIMESTAMP
            //1724380000000 = Friday, August 23, 2024 2:26:40 AM
            Assert.AreEqual(new DateTime(2024, 8, 23, 2, 26, 40, DateTimeKind.Utc), item.Date);

            // Sentence ID
            Assert.AreEqual("PA", item.Type);

            // Attribute Tags
            Assert.AreEqual(5, item.Tags.Count);
            Assert.AreEqual("1", item.Tags["r"]);
            Assert.AreEqual("143", item.Tags["s"]);
            Assert.AreEqual("PPE Mask", item.Tags["p"]);
            Assert.AreEqual("1", item.Tags["q"]);
            Assert.AreEqual("414", item.Tags["pr"]);

            // test without * char
            var sb = new StringBuilder();
            sb.Append("$1724380000000|PA|r:1|s:143|p:PPE Mask|q:1|pr:414");
            sb.AppendChecksum();

            var sentence2 = sb.ToString();
            Assert.AreEqual(sentence, sentence2);
        }

        [TestMethod]
        public void ToTelemetrySentence()
        {
            var item = new TelemItemDto()
            {
                Type = "rmc",
                Date = new DateTime(2024, 8, 21, 8, 15, 30, DateTimeKind.Utc)
            };

            item.Tags.Add("r", "1");
            item.Tags.Add("gn", "Mark");
            item.Tags.Add("cp", "Starlight");

            var expectedSentence = $"${item.Date.ToUnixTimeMilliseconds()}|rmc|";

            var sentence = item.ToTelemetrySentence();

            Assert.IsTrue(sentence.StartsWith(expectedSentence));
            Assert.IsTrue(sentence.Contains("|r:1"));
            Assert.IsTrue(sentence.Contains("|gn:Mark"));
            Assert.IsTrue(sentence.Contains("|cp:Starlight"));
        }
    }
}