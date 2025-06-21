// ================================================================================
// <copyright file="ItemTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Library.Tests.Base;

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ItemTests : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
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
            if (Debugger.IsAttached && isValid && !results.IsValid) { this.DebugLogResults(results, fileName); }

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
            var item = new ItemDto("test_type", "test_name", "test_name");

            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_name", item.Name);
            Assert.AreEqual("test_name", item.Key);


            item = new ItemDto("test_type", "test_name", "test_key");

            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_name", item.Name);
            Assert.AreEqual("test_key", item.Key);

            var attributes = new ItemTagsDto();
            attributes.Add("test_1", "test_1_value");
            
            item.Tags.Add(attributes);

            Assert.AreEqual("test_1_value", item.Tags["test_1"]);
            
            Assert.AreEqual("", item.Tags["INVALID_KEY"]);

            Assert.ThrowsException<ArgumentException>(() => new ItemDto("test", "Test", "INVALID!KEY!"));
        }

        [TestMethod]
        public void GetAttribute_Bool()
        {
            var attributes = new ItemTagsDto();

            // BOOL Tests
            attributes.Add("bool_false", "false");
            attributes.Add("bool_true", "true");
            
            attributes.Add("bool_False", "False");
            attributes.Add("bool_True", "True");

            Assert.AreEqual(4, attributes.Count);

            Assert.IsTrue(attributes.Get<bool>("bool_true", false));
            Assert.IsFalse(attributes.Get<bool>("bool_false", true));
            
            Assert.IsTrue(attributes.Get<bool>("bool_True", false));
            Assert.IsFalse(attributes.Get<bool>("bool_False", true));

            Assert.IsFalse(attributes.Get<bool>("bool_INVALID", false));
            Assert.IsTrue(attributes.Get<bool>("bool_INVALID", true));

            Assert.AreEqual(0, attributes.Get<int>("bool_true", 0));
        }

        [TestMethod]
        public void GetAttribute_Int()
        {
            var attributes = new ItemTagsDto();

            attributes.Add("int_77", "77");
            attributes.Add("int_-77", "-77");

            Assert.AreEqual(2, attributes.Count);

            // INT TESTS
            Assert.AreEqual(77, attributes.Get<int>("int_77", 0));
            Assert.AreEqual((decimal)77, attributes.Get<decimal>("int_77", 0));
        }

        [TestMethod]
        public void GetAttribute_Decimal()
        {
            var attributes = new ItemTagsDto();

            attributes.Add("77", "77");
            attributes.Add("7.7", "7.7");
            attributes.Add("-7.7", "-7.7");

            Assert.AreEqual(3, attributes.Count);

            // DECIMAL TESTS
            Assert.AreEqual(77, attributes.Get<decimal>("77", 0));
            Assert.AreEqual((decimal)7.7, attributes.Get<decimal>("7.7", 0));
            Assert.AreEqual((decimal)-7.7, attributes.Get<decimal>("-7.7", 0));

            // BAD DATA (Defaults)
            Assert.AreEqual(0, attributes.Get<int>("7.7", 0));
            Assert.IsTrue(attributes.Get<bool>("7.7", true));
        }

        [TestMethod]
        public void GetAttribute_TimeSpan()
        {
            var attributes = new ItemTagsDto();

            attributes.Add("070", "00:07:00");
            attributes.Add("077", "00:07:07");
            attributes.Add("7", "7");
            attributes.Add("7.7", "7.7");

            Assert.AreEqual(4, attributes.Count);

            // TIMESPAN TESTS
            Assert.AreEqual(TimeSpan.FromMinutes(7), attributes.Get<TimeSpan>("070", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(7 * 60 + 7), attributes.Get<TimeSpan>("077", TimeSpan.FromSeconds(0)));

            // BAD / INVALID
            Assert.AreEqual(TimeSpan.FromDays(7), attributes.Get<TimeSpan>("7", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(0), attributes.Get<TimeSpan>("7.7", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(0), attributes.Get<TimeSpan>("INVALID", TimeSpan.FromSeconds(0)));
        }

        [TestMethod]
        public void Parents()
        {
            var root = new ItemDto("root", "Root", "root");
            var child = new ItemDto("child", "Child", "child");
            var grandChild = new ItemDto("grand_child", "Grand Child", "grand_child");

            root.Attach(child);
            child.Attach(grandChild);

            // initialize links
            root.Init(null);

            Assert.AreSame(root, root.Root);
            Assert.AreSame(null, root.Parent);

            Assert.AreSame(root, child.Root);
            Assert.AreSame(root, child.Parent);

            Assert.AreSame(root, grandChild.Root);
            Assert.AreSame(child, grandChild.Parent);
        }

        [TestMethod]
        public void Attach_GetItem()
        {
            var root = new ItemDto("root", "Root", "root");
            var child = new ItemDto("child", "Child", "child");
            var grandChild = new ItemDto("grand_child", "Grand Child", "grand_child");

            root.Attach(child);
            child.Attach(grandChild);

            // initialize links
            root.Init(null);

            var testItem = root.Items[child.Key!];
            Assert.AreSame(testItem, child);
            Assert.AreSame(testItem.Parent, root);

            testItem = root.GetItem($"{child.Key}/{grandChild.Key}");
            Assert.IsNotNull(testItem);
            Assert.AreSame(testItem, grandChild);
            Assert.AreSame(testItem.Parent, child);

            testItem = root.GetItem($"{child.Key}/invalid_path");
            Assert.IsNull(testItem);

            testItem = root.GetItem($"{child.Key}//");
            Assert.IsNull(testItem);
        }

        [TestMethod]
        public void AttachItems_GetItem()
        {
            var root = new ItemDto("root", "Root", "root");
            
            var child1 = new ItemDto("child", "Child 1", "child_1");
            var child2 = new ItemDto("child", "Child 2", "child_2");
            var child3 = new ItemDto("child", "Child 3", "child_3");
            
            var children = new List<ItemDto> { child1, child2, child3 };
            root.Attach(children);

            Assert.AreEqual(3, root.Items.Count);
            
            var testItem = root.Items[child2.Key!];
            Assert.AreSame(testItem, child2);
            Assert.AreSame(testItem.Parent, root);
        }

        [TestMethod]
        public void Clone()
        {
            var root = new ItemDto("root", "Root", "root") { Id = Guid.NewGuid() };
            var child = new ItemDto("child", "Child", "child") { Id = Guid.NewGuid() };
            var grandChild = new ItemDto("grand_child", "Grand Child", "grand_child") { Id = Guid.NewGuid() };

            root["status"] = "Test Status 1";
            child["status"] = "Test Status 2";
            grandChild["status"] = "Test Status 3";

            root.Metadata["version"] = "1";
            child.Metadata["version"] = "2";
            grandChild.Metadata["version"] = "3";

            root.Attach(child);
            child.Attach(grandChild);

            root.Init(null);

            //CLONE TESTS
            var clone = child.Clone();

            Assert.AreNotSame(child, clone);
            Assert.AreNotSame(child.Root, clone.Root);
            Assert.IsNull(clone.Parent);

            Assert.AreEqual(child.Id, clone.Id);
            Assert.AreEqual(child["status"], clone["status"]);
            Assert.AreEqual(child.Metadata["version"], clone.Metadata["version"]);

            Assert.AreNotSame(child.Items["grand_child"], clone.Items["grand_child"]);            
        }

        [TestMethod]
        public void Remove()
        {
            var item = new ItemDto("item_type", "Item Name", "item_key") { Id = Guid.NewGuid() };

            item["status_1"] = "Test Status 1";
            item["status_2"] = "Test Status 2";

            Assert.AreEqual(2, item.Tags.Count);

            var result = item.Remove("INVALID");
            Assert.IsFalse(result);

            result = item.Remove("status_2");
            Assert.IsTrue(result);
            Assert.AreEqual(1, item.Tags.Count);

            item.RemoveAll();
            Assert.AreEqual(0, item.Tags.Count);
        }

        [TestMethod]
        public void Detatch()
        {
            var child = new ItemDto("child", "Child", "child_key") { Id = Guid.NewGuid() };
            var grandChild_1 = new ItemDto("grand_child", "Grand Child 1", "grand_child_1_key") { Id = Guid.NewGuid() };
            var grandChild_2 = new ItemDto("grand_child", "Grand Child 2", "grand_child_2_key") { Id = Guid.NewGuid() };

            child.Attach(grandChild_1);
            child.Attach(grandChild_2);
            child.Init(null);

            Assert.AreEqual(2, child.Items.Count);

            var result = child.Detatch("INVALID_KEY");
            Assert.IsFalse(result);

            result = child.Detatch("grand_child_1_key");
            Assert.IsTrue(result);
            Assert.AreEqual(1, child.Items.Count);
            Assert.AreEqual("grand_child_2_key", child.Items.First().Key);

            child.DetatchAll();
            Assert.AreEqual(0, child.Items.Count);
        }

        [TestMethod]
        public void Add_ByDictionary()
        {
            var item = new ItemDto("type", "name", "key");

            var attributes = new Dictionary<string, string>()
            {
                { "pin", "1777" },
                { "name", "Test Name" },
                { "location", "Basement" },
                { "finish", "Chrome" }
            };

            item.Add(attributes);

            foreach (var attribute in attributes)
            {
                Assert.AreEqual(attribute.Value, item[attribute.Key]);
            }
        }

        //[TestMethod]        
        //public void Add_ByObjectDictionary()
        //{
        //    var item = new ItemDto("type", "name");

        //    var testAttributes = new List<KeyValuePair<string, object>>()
        //    {
        //        new (AttributeDataTypes.String, "String Value"),
        //        new (AttributeDataTypes.Date, new DateOnly(2024, 1, 2)),
        //        new (AttributeDataTypes.Time, new TimeOnly(1, 2, 3, 4, 5)),
        //        new (AttributeDataTypes.DateTime, new DateTime(2024, 1, 2, 3, 4, 5, 6, 7)),
        //        new (AttributeDataTypes.DateTime, new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.FromHours(-5))),
        //        new (AttributeDataTypes.Decimal, new Decimal(1.23456)),
        //        new (AttributeDataTypes.Integer, 123456789),
        //        new (AttributeDataTypes.Url, new Uri("https://thingslibrary.io")),
        //        new (AttributeDataTypes.Boolean, true)
        //    };

        //    // check individual items
        //    foreach (var testAttribute in testAttributes)
        //    {
        //        var attributeDataType = AttributeDataTypes.Items[testAttribute.Key];

        //        var attribute = new ItemAttributeDto(testAttribute.Key, testAttribute.Value);

        //        Assert.AreEqual(attributeDataType.Key, attribute.Key);
        //    }

        //    // group add
        //    item.Add(testAttributes);

        //    foreach(var testAttribute in item.Tags)
        //    {
        //        var attributeDataType = AttributeDataTypes.Items[testAttribute.Key];

        //        var attribute = new ItemAttributeDto(testAttribute.Key, testAttribute.Value);

        //        Assert.AreEqual(attributeDataType.Key, attribute.Key);
        //    }            
        //}
    }
}