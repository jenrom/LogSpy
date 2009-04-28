using NUnit.Framework;

namespace LogSpy.Tests
{
    [TestFixture]
    public class StubTests
    {
        
        [Test]
        public void this_test_should_be_ignored()
        {
            Assert.Ignore();
        }

        [Test]
        public void this_test_should_ok()
        {
        }
    }
}