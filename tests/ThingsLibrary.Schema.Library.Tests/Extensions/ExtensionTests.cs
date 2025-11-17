// ================================================================================
// <copyright file="ExtensionTests.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Text;

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
        [DataRow(".test.key", ".test.key")]
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
    }
}