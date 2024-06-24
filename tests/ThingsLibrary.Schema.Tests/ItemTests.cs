using System.Globalization;
using System.Text.Json;
using Json.Schema;

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

            var results = schema.Evaluate(JsonDocument.Parse(json), Base.TestBase.EvaluationOptions);
            Assert.AreEqual(isValid, results.IsValid);
        }
    }
}