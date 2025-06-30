// ================================================================================
// <copyright file="TagDataTypeTests.cs" company="Starlight Software Co">
//    Copyright (c) Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

namespace ThingsLibrary.Schema.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TagDataTypesTests
    {
        [TestMethod]
        public void GetAll()
        {
            var items = ItemTagDataTypesDto.GetAll();
            Assert.AreEqual(19, items.Count);
        }

        [TestMethod]
        [DataRow(typeof(string), ItemTagDataTypesDto.String)]
        [DataRow(typeof(int), ItemTagDataTypesDto.Integer)]
        [DataRow(typeof(decimal), ItemTagDataTypesDto.Decimal)]
        [DataRow(typeof(DateTime), ItemTagDataTypesDto.DateTime)]
        [DataRow(typeof(DateTimeOffset), ItemTagDataTypesDto.Date)]
        [DataRow(typeof(DateOnly), ItemTagDataTypesDto.Date)]
        [DataRow(typeof(TimeOnly), ItemTagDataTypesDto.Time)]
        [DataRow(typeof(Uri), ItemTagDataTypesDto.Url)]
        [DataRow(typeof(bool), ItemTagDataTypesDto.Boolean)]
        [DataRow(typeof(TimeSpan), ItemTagDataTypesDto.Duration)]
        // default types
        [DataRow(typeof(long), ItemTagDataTypesDto.String)]
        [DataRow(typeof(double), ItemTagDataTypesDto.String)]
        [DataRow(typeof(TagDataTypesTests), ItemTagDataTypesDto.String)]
        public void GetType(Type type, string expectedKey)
        {
            var expectedItem = ItemTagDataTypesDto.Items[expectedKey];

            var testItem = ItemTagDataTypesDto.GetType(type);

            // just to hit all the getters 
            Assert.AreSame(expectedItem, testItem);
            Assert.AreEqual(expectedItem.Key, testItem.Key);
            Assert.AreEqual(expectedItem.Type, testItem.Type);
            Assert.AreEqual(expectedItem.Name, testItem.Name);
            Assert.AreEqual(expectedItem.Format, testItem.Format);
        }
    }
}
