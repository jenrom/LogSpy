using LogSpy.Core.Model;
using NUnit.Framework;
using LogSpy.Core.Model.LogFile;
using NUnit.Framework.SyntaxHelpers;

namespace LogSpy.Tests.Unit.Core.Model
{
    /// <summary>
    /// FileLogProviderFactoryTests.
    /// </summary>
    [TestFixture]
    public class FileLogProviderFactoryTests
    {
        [Test]
        public void Should_create_a_simple_file_log_provider_given_with_the_given_name_of_the_file_on_which_the_provider_is_based()
        {
            var sut = new FileLogProviderFactory();
            string filePath = "tests.log";
            ILogProvider provider = sut.CreateFor(new FileLogProviderCreationContext(filePath));
            Assert.That(provider, Is.TypeOf(typeof(FileLogProvider)));
            Assert.That(((FileLogProvider)provider).FilePath, Is.EqualTo(filePath));
        }
    }
}