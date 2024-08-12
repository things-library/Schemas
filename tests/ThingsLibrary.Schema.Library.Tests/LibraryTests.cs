using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.ComponentModel.DataAnnotations;

namespace ThingsLibrary.Schema.Library.Tests
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
        [DataRow("bad/version.json", false)]
        public void Validate(string fileName, bool isValid)
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
        [DataRow("valid/tree.json", true)]
        [DataRow("bad/tree_node_name.json", false)]
        [DataRow("bad/tree_node_type.json", false)]
        [DataRow("bad/version.json", false)]
        public void ValidateObjects(string fileName, bool isValid)
        {   
            // LOAD TEST JSON DATA
            var filePath = $"TestData/library/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);
                        
            // DESERIALIZE USING OBJECTS AND EVALUATE
            var item = doc.Deserialize<LibraryDto>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);

            var results = item.Validate(false);
            if (Debugger.IsAttached) { this.DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, !results.Any());
        }

        [TestMethod]
        public void Constructor_Library_Bad()
        {
            Assert.ThrowsException<ArgumentException>(() => new LibraryDto("BADKEY", "test"));
            Assert.ThrowsException<ArgumentException>(() => new LibraryDto("", "test"));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryDto(null, "test"));

            Assert.ThrowsException<ArgumentException>(() => new LibraryDto("test", ""));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryDto("test", null));
        }

        [TestMethod]
        public void Constructor_Library()
        {
            // basic
            var library = new LibraryDto();
            Assert.AreEqual(0, library.Validate().Count);   //nothing is required for a library


            // more complex
            library = new LibraryDto("test", "Test Library");

            Assert.AreEqual("test", library.Key);
            Assert.AreEqual("Test Library", library.Name);

            // initialize a item
            var item = new LibraryItemDto();
            Assert.AreEqual(3, item.Validate().Count);  //key, type and name are required
        }

        [TestMethod]
        public void Constructor_Item()
        {
            // initialize a item
            var item = new LibraryItemDto();
            Assert.AreEqual(3, item.Validate().Count);  //key, type and name are required

            item = new LibraryItemDto("item", "item_key", "Test Item");
            Assert.AreEqual("item", item.Type);
            Assert.AreEqual("item_key", item.Key);
            Assert.AreEqual("Test Item", item.Name);
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

            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("BAD", "key", "Name"));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto(null, "key", "Name"));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("", "key", "Name"));

            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "BAD", "Name"));
            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto("type", null, "Name"));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "", "Name"));

            Assert.ThrowsException<ArgumentNullException>(() => new LibraryItemDto("type", "key", null));
            Assert.ThrowsException<ArgumentException>(() => new LibraryItemDto("type", "key", ""));

        }

        //[TestMethod]
        //public void Library_BasicItemAdd()
        //{
        //    var library = new LibraryDto("test", "Test Library");

        //    var item = new BasicItemDto("TestKey", "TestType", "TestName");

        //    library.Add(item);
        //}

    }
}