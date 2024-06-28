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

        #endregion

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
        public void Validate(string fileName, bool isValid)
        {
            var schema = Base.TestBase.EvaluationOptions.SchemaRegistry.Get(LibrarySchemaUrl) as JsonSchema;
            Assert.IsNotNull(schema);

            // LOAD TEST JSON DATA
            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            // EVALUATE USING JSON SCHEMA
            var results = schema.Evaluate(doc, Base.TestBase.EvaluationOptions);
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
        [DataRow("valid/tree.json", true)]
        [DataRow("bad/tree_node_name.json", false)]
        [DataRow("bad/tree_node_type.json", false)]      
        
        public void ValidateObjects(string fileName, bool isValid)
        {   
            // LOAD TEST JSON DATA
            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);
                        
            // DESERIALIZE USING OBJECTS AND EVALUATE
            var item = doc.Deserialize<LibrarySchema>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);

            var results = item.Validate(false);
            if (Debugger.IsAttached) { this.DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, !results.Any());
        }
    }
}