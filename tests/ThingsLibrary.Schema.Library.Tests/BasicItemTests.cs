namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BasicItemTests
    {
        [TestMethod]
        public void Basic_Validation_Valid()
        {
            var testFilePaths = Directory.GetFiles($"TestData/basic-items/valid", "*.json");

            foreach (var testFilePath in testFilePaths)
            {
                var json = File.ReadAllText(testFilePath);
                var doc = JsonDocument.Parse(json);

                var item = doc.Deserialize<BasicItemDto>(SchemaBase.JsonSerializerOptions);
                Assert.IsNotNull(item);

                var validationErrors = item.Validate();
                Assert.IsFalse(validationErrors.Any());
            }
        }

        [TestMethod]
        public void Basic_Validation_Bad()
        {
            var testFilePaths = Directory.GetFiles($"TestData/basic-items/bad", "*.json");

            foreach (var testFilePath in testFilePaths)
            {
                var json = File.ReadAllText(testFilePath);
                var doc = JsonDocument.Parse(json);

                var item = doc.Deserialize<BasicItemDto>(SchemaBase.JsonSerializerOptions);
                Assert.IsNotNull(item);

                var validationErrors = item.Validate();
                Assert.IsTrue(validationErrors.Any());
            }
        }

        [TestMethod]
        public void Constructor()
        {
            var item = new BasicItemDto("test_type", "test_name");

            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_name", item.Name);
            Assert.AreEqual("test_name", item.Key);


            item = new BasicItemDto("test_type", "test_name", "test_key");

            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_name", item.Name);
            Assert.AreEqual("test_key", item.Key);

            var attributes = new BasicItemAttributesDto();
            attributes.Add("test_1", "test_1_value");
            attributes.Add("test_2", new List<string> { "test_2_value_1", "test_2_value_2" });

            item.Attributes.Add(attributes);

            Assert.AreEqual("test_1_value", item.Attributes["test_1"]);
            Assert.AreEqual("test_1_value", item.Attributes["test_1"]);

            Assert.AreEqual("", item.Attributes["INVALID_KEY"]);
        }

        [TestMethod]
        public void GetAttribute_Bool()
        {
            var attributes = new BasicItemAttributesDto();

            // BOOL Tests
            attributes.Add("bool_false", "false");
            attributes.Add("bool_true", "true");
            attributes.Add("bool_true_false", new List<string> { "true", "false" });

            attributes.Add("bool_False", "False");
            attributes.Add("bool_True", "True");

            Assert.AreEqual(5, attributes.Count);

            Assert.IsTrue(attributes.Get<bool>("bool_true", false));
            Assert.IsFalse(attributes.Get<bool>("bool_false", true));
            Assert.IsTrue(attributes.Get<bool>("bool_true_false", false));  //should return the first of the array

            Assert.IsTrue(attributes.Get<bool>("bool_True", false));
            Assert.IsFalse(attributes.Get<bool>("bool_False", true));

            Assert.IsFalse(attributes.Get<bool>("bool_INVALID", false));
            Assert.IsTrue(attributes.Get<bool>("bool_INVALID", true));

            Assert.AreEqual(0, attributes.Get<int>("bool_true", 0));
        }

        [TestMethod]
        public void GetAttribute_Int()
        {
            var attributes = new BasicItemAttributesDto();

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
            var attributes = new BasicItemAttributesDto();

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
            var attributes = new BasicItemAttributesDto();

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
            var root = new BasicItemDto("root", "root", "Root");
            var child = new BasicItemDto("child", "child", "Child");
            var grandChild = new BasicItemDto("grand_child", "grand_child", "Grand Child");

            root.Attachments.Add(child);
            child.Attachments.Add(grandChild);

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
        public void Clone()
        {
            var root = new BasicItemDto("root", "root", "Root") { Id = Guid.NewGuid() };
            var child = new BasicItemDto("child", "child", "Child") { Id = Guid.NewGuid() };
            var grandChild = new BasicItemDto("grand_child", "grand_child", "Grand Child") { Id = Guid.NewGuid() };

            root["status"] = "Test Status 1";
            child["status"] = "Test Status 2";
            grandChild["status"] = "Test Status 3";

            root.Metadata["version"] = "1";
            child.Metadata["version"] = "2";
            grandChild.Metadata["version"] = "3";

            root.Attachments.Add(child);
            child.Attachments.Add(grandChild);

            root.Init(null);

            //CLONE TESTS
            var clone = child.Clone();

            Assert.AreNotSame(child, clone);
            Assert.AreNotSame(child.Root, clone.Root);
            Assert.IsNull(clone.Parent);

            Assert.AreEqual(child.Id, clone.Id);
            Assert.AreEqual(child["status"], clone["status"]);
            Assert.AreEqual(child.Metadata["version"], clone.Metadata["version"]);

            Assert.AreNotSame(child.Attachments[0], clone.Attachments[0]);
            Assert.AreEqual(child.Attachments[0].Key, clone.Attachments[0].Key);
        }

        [TestMethod]
        public void Remove()
        {
            var item = new BasicItemDto("item_type", "Item Name", "item_key") { Id = Guid.NewGuid() };

            item["status_1"] = "Test Status 1";
            item["status_2"] = "Test Status 2";

            Assert.AreEqual(2, item.Attributes.Count);

            var result = item.Remove("INVALID");
            Assert.IsFalse(result);

            result = item.Remove("status_2");
            Assert.IsTrue(result);
            Assert.AreEqual(1, item.Attributes.Count);

            item.RemoveAll();
            Assert.AreEqual(0, item.Attributes.Count);
        }

        [TestMethod]
        public void Detatch()
        {
            var child = new BasicItemDto("child", "Child", "child_key") { Id = Guid.NewGuid() };
            var grandChild_1 = new BasicItemDto("grand_child", "Grand Child 1", "grand_child_1_key") { Id = Guid.NewGuid() };
            var grandChild_2 = new BasicItemDto("grand_child", "Grand Child 2", "grand_child_2_key") { Id = Guid.NewGuid() };

            child.Attach(grandChild_1);
            child.Attach(grandChild_2);
            child.Init(null);

            Assert.AreEqual(2, child.Attachments.Count);

            var result = child.Detatch("INVALID_KEY");
            Assert.IsFalse(result);

            result = child.Detatch("grand_child_1_key");
            Assert.IsTrue(result);
            Assert.AreEqual(1, child.Attachments.Count);
            Assert.AreEqual("grand_child_2_key", child.Attachments[0].Key);

            child.DetatchAll();
            Assert.AreEqual(0, child.Attachments.Count);
        }

        [TestMethod]
        public void Add_Dictionary()
        {
            var item = new BasicItemDto("type", "name");

            var attributes = new Dictionary<string, string>()
            {
                { "pin", "1777" },
                { "name", "Test Name" },
                { "location", "Basement" },
                { "finish", "Chrome" }
            };

            item.Add(attributes);

            foreach(var attribute in attributes)
            {
                Assert.AreEqual(attribute.Value, item[attribute.Key]);
            }
        }
    }
}