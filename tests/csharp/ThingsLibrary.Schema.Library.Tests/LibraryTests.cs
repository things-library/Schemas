// ================================================================================
// <copyright file="LibraryTests.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LibraryTests : Base.TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // static field initialization
            LoadSchemas();
        }

        /// <summary>
        /// This tests json documents against the json schema
        /// </summary>
        /// <param name="fileName">Test Json Document</param>
        /// <param name="isValid">Expected Validation Result</param>
        [TestMethod]
        [DataRow("valid/basic.json", true)]
        [DataRow("valid/minimal.json", true)]        
        [DataRow("valid/tree.json", true)]
        [DataRow("bad/tree_node_name.json", false)]
        [DataRow("bad/tree_node_type.json", false)]
        [DataRow("bad/item_key_pattern1.json", false)]
        [DataRow("bad/item_key_pattern2.json", false)]
        [DataRow("bad/item_key_pattern3.json", false)]
        [DataRow("bad/type_key_pattern1.json", false)]
        [DataRow("bad/type_key_pattern2.json", false)]
        [DataRow("bad/type_key_pattern3.json", false)]
        [DataRow("bad/empty.json", false)]
        [DataRow("bad/items_missing.json", false)]
        [DataRow("bad/types_missing.json", false)]
        public void SchemaValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            // EVALUATE USING JSON SCHEMA
            var results = Base.TestBase.ItemSchemaDoc.Evaluate(doc, Base.TestBase.EvaluationOptions);
            if (Debugger.IsAttached && isValid && !results.IsValid) { DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, results.IsValid);
        }

        /// <summary>
        /// This tests the object validation definitions
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isValid"></param>
        /// <remarks>The lack of something like 'items' and 'types' is not checked as we don't want to always check for nullable objects and instead initialize as a empty collection</remarks>
        [TestMethod]
        [DataRow("valid/basic.json", true)]
        [DataRow("valid/minimal.json", true)]        
        [DataRow("valid/tree.json", true)]
        [DataRow("bad/tree_node_name.json", false)]
        //[DataRow("bad/tree_node_type.json", false)]
        public void ClassValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            // DESERIALIZE USING OBJECTS AND EVALUATE
            var library = doc.Deserialize<RootItemDto>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(library);

            var results = library.Validate(false);
            if (Debugger.IsAttached && isValid && results.Any()) { this.DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, !results.Any());
        }
    }
}