using System.Text.Json;
using Json.Schema;

using ThingsLibrary.Schema.Base;

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
        [DataRow("valid/children.json", true)]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("bad/attribute_value.json", false)]
        [DataRow("bad/missing_name.json", false)]
        [DataRow("bad/missing_type.json", false)]
        [DataRow("bad/name.json", false)]
        [DataRow("bad/type.json", false)]
        public void Validate(string fileName, bool isValid)
        {
            var schema = Base.TestBase.EvaluationOptions.SchemaRegistry.Get(ItemSchemaUrl) as JsonSchema;
            Assert.IsNotNull(schema);

            var filePath = $"TestData/items/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var results = schema.Evaluate(doc, Base.TestBase.EvaluationOptions);
            Assert.AreEqual(isValid, results.IsValid);

            var item = doc.Deserialize<ItemSchema>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);
        }

        //[TestMethod]
        //[DataRow("valid/children.json", true)]
        //[DataRow("valid/minimal.json", true)]
        //[DataRow("valid/simple.json", true)]
        //[DataRow("bad/attribute_value.json", false)]
        //[DataRow("bad/missing_name.json", false)]
        //[DataRow("bad/missing_type.json", false)]
        //[DataRow("bad/name.json", false)]
        //[DataRow("bad/type.json", false)]
        //public void ValidateObjects(string fileName, bool isValid)
        //{
        //    var schema = Base.TestBase.EvaluationOptions.SchemaRegistry.Get(ItemSchemaUrl) as JsonSchema;
        //    Assert.IsNotNull(schema);

        //    var filePath = $"TestData/items/{fileName}";
        //    Assert.IsTrue(File.Exists(filePath));

        //    var json = File.ReadAllText(filePath);
        //    var doc = JsonDocument.Parse(json);

        //    var results = schema.Evaluate(doc, Base.TestBase.EvaluationOptions);
        //    Assert.AreEqual(isValid, results.IsValid);

        //    var item = doc.Deserialize<ItemSchema>(SchemaBase.JsonSerializerOptions);
        //    Assert.IsNotNull(item);
        //}
    }
}