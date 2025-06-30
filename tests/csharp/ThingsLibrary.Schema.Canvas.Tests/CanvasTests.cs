// ================================================================================
// <copyright file="CanvasTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Canvas;
using ThingsLibrary.Schema.Canvas.Base;
using ThingsLibrary.Schema.Canvas.Tests.Base;

namespace ThingsLibrary.Schema.Canvas.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CanvasTests : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // static field initialization
            LoadSchemas();
        }

        [TestMethod]
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("bad/minimal.json", false)]       
        public void SchemaValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            // EVALUATE USING JSON SCHEMA
            var results = Base.TestBase.CanvasSchemaDoc.Evaluate(doc, Base.TestBase.EvaluationOptions);
            if (Debugger.IsAttached && isValid && !results.IsValid) { DebugLogResults(results, fileName); }

            Assert.AreEqual(isValid, results.IsValid);
        }

        [TestMethod]        
        [DataRow("valid/minimal.json", true)]
        [DataRow("valid/simple.json", true)]
        [DataRow("bad/minimal.json", false)]        
        public void ClassValidation(string fileName, bool isValid)
        {
            // LOAD TEST JSON DATA
            var filePath = $"TestData/{fileName}";
            Assert.IsTrue(File.Exists(filePath));

            var json = File.ReadAllText(filePath);
            var doc = JsonDocument.Parse(json);

            var item = doc.Deserialize<CanvasRoot>(SchemaBase.JsonSerializerOptions);
            Assert.IsNotNull(item);

            var validationErrors = item.Validate();
            if (Debugger.IsAttached && isValid && validationErrors.Any()) { this.DebugLogResults(validationErrors, fileName); }

            Assert.AreEqual(!isValid, validationErrors.Any());
        }             
    }
}