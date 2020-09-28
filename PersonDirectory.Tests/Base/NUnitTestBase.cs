using NUnit.Framework;

namespace PersonDirectory.Tests
{
    [TestFixture]
    public class NUnitTestBase
    {
        
        [SetUp]
        public virtual void Setup()
        {
        }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        public virtual void GlobalTeardown()
        {

        }
    }
}
