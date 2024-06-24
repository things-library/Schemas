using Json.Schema;
using System.Text.Json;

namespace ThingsLibrary.Schema.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LibraryTests : Base.TestBase
    {
        #region --- Setup/Cleanup ---

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // static field initialization
            LoadSchemas();
        }

        //[TestInitialize]
        //public void TestInitialize()
        //{

        //}

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
        [DataRow("valid/minimal.json", true)]
        [DataRow("bad/empty.json", false)]
        [DataRow("bad/items_missing.json", false)]
        [DataRow("bad/types_key_length.json", false)]
        [DataRow("bad/types_key_pattern1.json", false)]
        [DataRow("bad/types_key_pattern2.json", false)]
        [DataRow("bad/types_key_pattern3.json", false)]
        [DataRow("bad/types_missing.json", false)]
        public void Validate(string fileName, bool isValid)
        {
            var schema = Base.TestBase.EvaluationOptions.SchemaRegistry.Get(LibrarySchemaUrl) as JsonSchema;
            Assert.IsNotNull(schema);

            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);

            var results = schema.Evaluate(JsonDocument.Parse(json), Base.TestBase.EvaluationOptions);
            Assert.AreEqual(isValid, results.IsValid);
        }
    }
}