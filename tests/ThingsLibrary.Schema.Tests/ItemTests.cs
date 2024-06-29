namespace ThingsLibrary.Schema.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ItemTests : Base.TestBase
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
            var filePath = $"TestData/items/{fileName}";
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
            var filePath = $"TestData/items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var item = doc.Deserialize<ItemBasicSchema>(SchemaBase.JsonSerializerOptions);
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
    }
}