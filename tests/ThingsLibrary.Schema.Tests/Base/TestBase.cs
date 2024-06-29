using System.ComponentModel.DataAnnotations;

namespace ThingsLibrary.Schema.Tests.Base
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TestBase
    {
        public static Uri ItemSchemaUrl { get; } = new Uri("https://schema.thingslibrary.io/1.0/item.json");
        public static Uri LibrarySchemaUrl { get; } = new Uri("https://schema.thingslibrary.io/1.0/library.json");

        public static JsonSchema ItemSchemaDoc { get; set; }
        public static JsonSchema LibrarySchemaDoc { get; set; }

        public static EvaluationOptions EvaluationOptions = new EvaluationOptions { OutputFormat = OutputFormat.List };



        public static void LoadSchemas()
        {
            // https://docs.json-everything.net/schema/examples/external-schemas/

            var schemaFolderPath = "Schemas/1.0";
            Assert.IsTrue(Directory.Exists(schemaFolderPath));

            Console.WriteLine("Loading Item Schemas...");
            var schemaDoc = JsonSchema.FromFile("Schemas/1.0/item.json");
            SchemaRegistry.Global.Register(schemaDoc);
            TestBase.ItemSchemaDoc = schemaDoc;

            Console.WriteLine("Loading Item Schemas...");
            schemaDoc = JsonSchema.FromFile("Schemas/1.0/library.json");
            SchemaRegistry.Global.Register(schemaDoc);
            TestBase.LibrarySchemaDoc = schemaDoc;            
        }

        public void DebugLogResults(EvaluationResults? results, string filename)
        {
            // nothing to see here
            if (results == null || results.IsValid) { return; }

            var errors = results.Details.Where(x => !x.IsValid && x.HasErrors).ToList();
            if (Debugger.IsAttached && errors.Any())
            {
                Debug.WriteLine("================================================================================");
                Debug.WriteLine($" Evaluation Errors (File: {filename})");
                Debug.WriteLine("================================================================================");
                foreach (var error in errors)
                {
                    if (error.Errors == null) { continue; }
                    
                    Debug.WriteLine("Node:  " + error.InstanceLocation);
                    Debug.WriteLine("Error: " + string.Join("; ", error.Errors.Values));
                    Debug.WriteLine("");
                }
            }
        }

        public void DebugLogResults(ICollection<ValidationResult> results, string filename)
        {
            if (!results.Any()) { return; }

            Debug.WriteLine("================================================================================");
            Debug.WriteLine($" Evaluation Errors (File: {filename})");
            Debug.WriteLine("================================================================================");
            this.DebugLogResults(results);

        }

        public void DebugLogResults(ICollection<ValidationResult> results)
        {
            if (!results.Any()) { return; }
            
            foreach (var error in results)
            {
                Debug.WriteLine("Members:  " + string.Join("; ", error.MemberNames));
                Debug.WriteLine("Error: " + error.ErrorMessage);
                Debug.WriteLine("");
            }

        }
    }
}