using ThingsLibrary.Schema.Library;

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AttributeDataTypesTests
    {
        [TestMethod]
        public void GetAll()
        {
            var items = AttributeDataTypes.GetAll();
            Assert.AreEqual(19, items.Count);
        }

        [TestMethod]
        [DataRow(typeof(string), AttributeDataTypes.String)]
        [DataRow(typeof(int), AttributeDataTypes.Integer)]
        [DataRow(typeof(decimal), AttributeDataTypes.Decimal)]
        [DataRow(typeof(DateTime), AttributeDataTypes.DateTime)]
        [DataRow(typeof(DateTimeOffset), AttributeDataTypes.Date)]
        [DataRow(typeof(DateOnly), AttributeDataTypes.Date)]
        [DataRow(typeof(TimeOnly), AttributeDataTypes.Time)]
        [DataRow(typeof(Uri), AttributeDataTypes.Url)]
        [DataRow(typeof(bool), AttributeDataTypes.Boolean)]
        [DataRow(typeof(TimeSpan), AttributeDataTypes.Duration)]
        // default types
        [DataRow(typeof(long), AttributeDataTypes.String)]
        [DataRow(typeof(double), AttributeDataTypes.String)]
        [DataRow(typeof(AttributeDataTypesTests), AttributeDataTypes.String)]
        public void GetType(Type type, string expectedKey)
        {
            var expectedItem = AttributeDataTypes.Items[expectedKey];

            var testItem = AttributeDataTypes.GetType(type);

            // just to hit all the getters 
            Assert.AreSame(expectedItem, testItem);
            Assert.AreEqual(expectedItem.Key, testItem.Key);
            Assert.AreEqual(expectedItem.Type, testItem.Type);
            Assert.AreEqual(expectedItem.Name, testItem.Name);
            Assert.AreEqual(expectedItem.Format, testItem.Format);
        }
    }
}
