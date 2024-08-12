using ThingsLibrary.Schema.Library.Extensions;

namespace ThingsLibrary.Schema.Library.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtensionTests
    {
        [TestMethod]
        [DataRow("test_key", "test_key")]   //already valid
        [DataRow("test-key", "test-key")]   //already valid
        [DataRow("000_000", "000_000")]     //already valid
        [DataRow("000-000", "000-000")]     //already valid
        [DataRow("Test Key", "test_key")]
        [DataRow("TestKey", "test_key")]
        [DataRow("testKey", "test_key")]
        [DataRow("Test/Key", "test_key")]
        [DataRow("Test/key", "test_key")]
        [DataRow("/Test/key", "test_key")]
        [DataRow("Test.Key", "test_key")]
        [DataRow("Test.key", "test_key")]
        [DataRow(".test.key", "test_key")]
        [DataRow(".test././\\key", "test_key")]

        public void ToKey(string input, string expected)
        {
            var result = input.ToKey();

            Assert.AreEqual(expected, result);            
        }        
    }
}