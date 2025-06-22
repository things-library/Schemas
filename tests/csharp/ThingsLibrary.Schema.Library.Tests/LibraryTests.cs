// ================================================================================
// <copyright file="LibraryTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LibraryTests : Base.TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
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
        [DataRow("valid/minimal2.json", true)]
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
            var results = Base.TestBase.LibrarySchemaDoc.Evaluate(doc, Base.TestBase.EvaluationOptions);
            if (Debugger.IsAttached && isValid && !results.IsValid) { this.DebugLogResults(results, fileName); }

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
        [DataRow("valid/minimal2.json", true)]
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
            var library = doc.Deserialize<LibraryDto>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(library);

            var results = library.Validate(false);
            if (Debugger.IsAttached && isValid && results.Any()) { this.DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, !results.Any());
        }

        [TestMethod]
        public void Constructor_Library_Bad()
        {
            Assert.ThrowsException<ArgumentException>(() => new LibraryDto(""));
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryDto(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        public void Constructor_Library()
        {
            // basic
            var library = new LibraryDto();
            
            var results = library.Validate();
            Assert.AreEqual(0, results.Count);   //nothing is required for a library

            // more complex
            library = new LibraryDto("Test Library");                        
            Assert.AreEqual("Test Library", library.Name);            
        }

        [TestMethod]
        public void Constructor_Item()
        {
            // initialize a item
            var item = new LibraryItemDto();
            Assert.AreEqual(3, item.Validate().Count);

            item = new LibraryItemDto("item", "item_key", "Test Item");
            Assert.AreEqual("item", item.Type);
            Assert.AreEqual("item_key", item.Key);
            Assert.AreEqual("Test Item", item.Name);
            Assert.AreEqual(0, item.Validate().Count);
        }

        [TestMethod]
        public void Constructor_Item_Bad()
        {
            var item = new LibraryItemDto("item", "item_key", "Test Item");

            // BAD VALUES
            item.Type = "BAD";
            Assert.AreEqual(1, item.Validate().Count);
            item.Key = "BAD";
            Assert.AreEqual(2, item.Validate().Count);
            item.Name = "";
            Assert.AreEqual(3, item.Validate().Count);

            #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("BAD", "key", "Name"));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto(null, "key", "Name"));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("", "key", "Name"));

            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "BAD", "Name"));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto("type", null, "Name"));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "", "Name"));

            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto("type", "key", null));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "key", ""));
            #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}