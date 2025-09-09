// ================================================================================
// <copyright file="TestBase.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.ComponentModel.DataAnnotations;

namespace ThingsLibrary.Schema.Canvas.Tests.Base
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TestBase
    {
        public static Uri ItemSchemaUrl { get; } = new Uri("https://schema.thingslibrary.io/1.2/library.json");
        
        public static JsonSchema CanvasSchemaDoc { get; set; } = JsonSchema.Empty;
        
        public static EvaluationOptions EvaluationOptions = new () { OutputFormat = OutputFormat.List };


        public static void LoadSchemas()
        {
            // https://docs.json-everything.net/schema/examples/external-schemas/

            string schemaFilePath = "Schemas/1.1/canvas.json";
            Assert.IsTrue(File.Exists(schemaFilePath));

            Console.WriteLine("Loading Item Schemas...");
            TestBase.CanvasSchemaDoc = JsonSchema.FromFile(schemaFilePath);

            SchemaRegistry.Global.Register(TestBase.CanvasSchemaDoc);
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