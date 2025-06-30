// ================================================================================
// <copyright file="ItemTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Tests.Base;

namespace ThingsLibrary.Schema.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ItemTests : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // static field initialization
            LoadSchemas();
        }

        [TestMethod]
        [DataRow("bad/key_max.json", false)]
        [DataRow("bad/key_min.json", false)]
        [DataRow("bad/name_max.json", false)]
        [DataRow("bad/name_min.json", false)]
        [DataRow("bad/type_max.json", false)]
        [DataRow("bad/type_min.json", false)]
        [DataRow("bad/type_missing.json", false)]

        [DataRow("valid/attribute_value.json", true)]
        [DataRow("valid/date.json", true)]
        [DataRow("valid/item_children.json", true)]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/name_max.json", true)]
        [DataRow("valid/name_min.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("valid/type_max.json", true)]
        [DataRow("valid/type_min.json", true)]
        public void SchemaValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            // EVALUATE USING JSON SCHEMA
            var results = Base.TestBase.ItemSchemaDoc.Evaluate(doc, Base.TestBase.EvaluationOptions);
            if (Debugger.IsAttached && isValid && !results.IsValid) { DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, results.IsValid);
        }

        [TestMethod]
        [DataRow("bad/key_max.json", false)]
        [DataRow("bad/key_min.json", false)]
        [DataRow("bad/name_max.json", false)]
        [DataRow("bad/name_min.json", false)]
        [DataRow("bad/type_max.json", false)]
        [DataRow("bad/type_min.json", false)]
        [DataRow("bad/type_missing.json", false)]
        
        [DataRow("valid/attribute_value.json", true)]
        [DataRow("valid/date.json", true)]
        [DataRow("valid/item_children.json", true)]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/name_max.json", true)]
        [DataRow("valid/name_min.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("valid/type_max.json", true)]
        [DataRow("valid/type_min.json", true)]
        public void ClassValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var item = doc.Deserialize<ItemDto>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);

            var validationErrors = item.Validate();
            if (Debugger.IsAttached && isValid && validationErrors.Any()) { this.DebugLogResults(validationErrors, fileName); }

            Assert.AreEqual(!isValid, validationErrors.Any());
        }

        [TestMethod]
        public void Constructor()
        {
            var item = new RootItemDto() { Type = "test_type", Name = "test_name", Key = "test_key" };

            var validationErrors = item.Validate();

            if (Debugger.IsAttached && validationErrors.Any()) { this.DebugLogResults(validationErrors); }
            Assert.IsFalse(validationErrors.Any());

            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_name", item.Name);
            Assert.AreEqual("test_key", item.Key);
                                    
            item.Tags.Add("test_1", "Test Value");

            Assert.AreEqual("Test Value", item.Tags["test_1"]);
                        
            Assert.ThrowsException<KeyNotFoundException>(() => item.Tags["INVALID_KEY"]);
        }
        

        [TestMethod]
        public void Clone()
        {
            var root = new RootItemDto() { Type = "root", Name = "Root", Key = "root" };
            var child = new RootItemDto() { Type = "child", Name = "Child", Key = "child" };
            var grandChild = new RootItemDto() { Type = "grand_child", Name = "Grand Child", Key = "grand_child" };

            root.Tags["status"] = "Test Status 1";
            child.Tags["status"] = "Test Status 2";
            grandChild.Tags["status"] = "Test Status 3";

            root.Meta["version"] = "1";
            child.Meta["version"] = "2";
            grandChild.Meta["version"] = "3";

            root.Items.Add(child.Key, child);
            child.Items.Add(grandChild.Key, grandChild);
            
            //CLONE TESTS
            var clone = child.Clone();

            Assert.AreNotSame(child, clone);
            
            Assert.AreEqual(child.Key, clone.Key);
            Assert.AreEqual(child.Tags["status"], clone.Tags["status"]);
            Assert.AreEqual(child.Meta["version"], clone.Meta["version"]);

            Assert.AreNotSame(child.Items["grand_child"], clone.Items["grand_child"]);            
        }

        [TestMethod]
        public void Remove()
        {
            var item = new RootItemDto() { Type = "item_type", Name = "Item Name", Key = "item_key" };

            item.Tags["status_1"] = "Test Status 1";
            item.Tags["status_2"] = "Test Status 2";

            Assert.AreEqual(2, item.Tags.Count);

            var result = item.Tags.Remove("INVALID");
            Assert.IsFalse(result);

            result = item.Tags.Remove("status_2");
            Assert.IsTrue(result);
            Assert.AreEqual(1, item.Tags.Count);

            item.Tags.Clear();
            Assert.AreEqual(0, item.Tags.Count);
        }       
    }
}