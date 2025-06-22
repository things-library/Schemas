// ================================================================================
// <copyright file="TagDataTypeTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TagDataTypesTests
    {
        [TestMethod]
        public void GetAll()
        {
            var items = TagDataTypes.GetAll();
            Assert.AreEqual(19, items.Count);
        }

        [TestMethod]
        [DataRow(typeof(string), TagDataTypes.String)]
        [DataRow(typeof(int), TagDataTypes.Integer)]
        [DataRow(typeof(decimal), TagDataTypes.Decimal)]
        [DataRow(typeof(DateTime), TagDataTypes.DateTime)]
        [DataRow(typeof(DateTimeOffset), TagDataTypes.Date)]
        [DataRow(typeof(DateOnly), TagDataTypes.Date)]
        [DataRow(typeof(TimeOnly), TagDataTypes.Time)]
        [DataRow(typeof(Uri), TagDataTypes.Url)]
        [DataRow(typeof(bool), TagDataTypes.Boolean)]
        [DataRow(typeof(TimeSpan), TagDataTypes.Duration)]
        // default types
        [DataRow(typeof(long), TagDataTypes.String)]
        [DataRow(typeof(double), TagDataTypes.String)]
        [DataRow(typeof(TagDataTypesTests), TagDataTypes.String)]
        public void GetType(Type type, string expectedKey)
        {
            var expectedItem = TagDataTypes.Items[expectedKey];

            var testItem = TagDataTypes.GetType(type);

            // just to hit all the getters 
            Assert.AreSame(expectedItem, testItem);
            Assert.AreEqual(expectedItem.Key, testItem.Key);
            Assert.AreEqual(expectedItem.Type, testItem.Type);
            Assert.AreEqual(expectedItem.Name, testItem.Name);
            Assert.AreEqual(expectedItem.Format, testItem.Format);
        }
    }
}
