namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BasicItemTests : Base.TestBase
    {
        #region --- Setup/Cleanup ---

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // static field initialization
            LoadSchemas();
        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    //nothing
        //}

        //[ClassCleanup]
        //public void ClassCleanup()
        //{
        //    //nothing
        //}

        #endregion


        [TestMethod]
        [DataRow("valid/attribute_value.json", true)]
        [DataRow("valid/attribute_values.json", true)]
        [DataRow("valid/children.json", true)]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/name_max.json", true)]
        [DataRow("valid/name_min.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("valid/type_max.json", true)]
        [DataRow("valid/type_min.json", true)]
        [DataRow("bad/attribute_value_1.json", false)]
        [DataRow("bad/attribute_value_2.json", false)]
        [DataRow("bad/missing_name.json", false)]
        [DataRow("bad/missing_type.json", false)]
        [DataRow("bad/name_max.json", false)]
        [DataRow("bad/name_min.json", false)]
        [DataRow("bad/type_max.json", false)]
        [DataRow("bad/type_min.json", false)]
        public void Validate(string fileName, bool isValid)
        {            
            var filePath = $"TestData/basic-items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var results = Base.TestBase.ItemSchemaDoc.Evaluate(doc, Base.TestBase.EvaluationOptions);
            if (Debugger.IsAttached && isValid && !results.IsValid) { this.DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, results.IsValid);
        }

        [TestMethod]
        [DataRow("valid/children.json", true)]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/name_max.json", true)]
        [DataRow("valid/name_min.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("valid/type_max.json", true)]
        [DataRow("valid/type_min.json", true)]
        [DataRow("bad/missing_name.json", false)]
        [DataRow("bad/missing_type.json", false)]
        [DataRow("bad/type_max.json", false)]
        [DataRow("bad/type_min.json", false)]
        public void ValidateObjects(string fileName, bool isValid)
        {
            var filePath = $"TestData/basic-items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var item = doc.Deserialize<BasicItemDto>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);

            var validationErrors = item.Validate();
            if (isValid)
            {
                // if we expect valid then there would be not validation errors
                Assert.IsFalse(validationErrors.Any());
            }
            else
            {
                Assert.IsTrue(validationErrors.Any());
            }
        }

        [TestMethod]        
        public void Constructor()
        {
            var item = new BasicItemDto("test_key", "test_type");

            Assert.AreEqual("test_key", item.Key);
            Assert.AreEqual("test_type", item.Type);
            Assert.AreEqual("test_key", item.Name);

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

            Assert.IsTrue(attributes.Get<bool>("bool_true", false));
            Assert.IsFalse(attributes.Get<bool>("bool_false", true));
            Assert.IsTrue(attributes.Get<bool>("bool_true_false", false));  //should return the first of the array

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

            // TIMESPAN TESTS
            Assert.AreEqual(TimeSpan.FromMinutes(7), attributes.Get<TimeSpan>("070", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(7 * 60 + 7), attributes.Get<TimeSpan>("077", TimeSpan.FromSeconds(0)));

            // BAD / INVALID
            Assert.AreEqual(TimeSpan.FromDays(7), attributes.Get<TimeSpan>("7", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(0), attributes.Get<TimeSpan>("7.7", TimeSpan.FromSeconds(0)));
            Assert.AreEqual(TimeSpan.FromSeconds(0), attributes.Get<TimeSpan>("INVALID", TimeSpan.FromSeconds(0)));
        }
    }
}