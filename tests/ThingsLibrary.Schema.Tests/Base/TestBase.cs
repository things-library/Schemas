using Json.Schema;

namespace ThingsLibrary.Schema.Tests.Base
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TestBase
    {
        public static Uri ItemSchemaUrl { get; } = new Uri("https://schema.thingslibrary.io/1.0/item.json");
        public static Uri LibrarySchemaUrl { get; } = new Uri("https://schema.thingslibrary.io/1.0/library.json");

        public static JsonSchema ItemSchemaDoc { get; set; }
        public static JsonSchema LibrarySchemaDoc { get; set; }

        public static EvaluationOptions EvaluationOptions = new EvaluationOptions { OutputFormat = OutputFormat.Hierarchical };



        public static void LoadSchemas()
        {
            // https://docs.json-everything.net/schema/examples/external-schemas/

            var schemaFolderPath = "TestData/schemas";
            Assert.IsTrue(Directory.Exists(schemaFolderPath));

            Console.WriteLine("Loading Schemas...");

            var filePaths = Directory.GetFiles("TestData/schemas", "*.json");
            foreach (var filePath in filePaths)
            {                
                Console.WriteLine($"+ {Path.GetFileName(filePath)}");

                var schema = JsonSchema.FromFile(filePath);
                SchemaRegistry.Global.Register(schema);
            }

            TestBase.ItemSchemaDoc = (TestBase.EvaluationOptions.SchemaRegistry.Get(ItemSchemaUrl) as JsonSchema)!;
            TestBase.LibrarySchemaDoc = (TestBase.EvaluationOptions.SchemaRegistry.Get(LibrarySchemaUrl) as JsonSchema)!;
        }
    }
}