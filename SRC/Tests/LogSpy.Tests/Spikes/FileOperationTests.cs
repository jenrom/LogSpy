using System;
using System.IO;
using NUnit.Framework;

namespace LogSpy.Tests.Spikes
{
    [TestFixture]
    [Category("Spikes")]
    public class FileOperationTests
    {
        private string name = "test.txt";

        [SetUp]
        public void before_each()
        {
            try
            {
                File.Delete(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        [Test]
        public void should_be_able_to_read_and_write()
        {
            Console.WriteLine("Creating");

            using (var writeStream = new FileStream(name, FileMode.Append, FileAccess.Write, FileShare.Read)) 
            {
                using (var readStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    var reader = new StreamReader(readStream);
                    var writer = new StreamWriter(writeStream);
                    writer.WriteLine("test");
                    writer.Flush();
                    Console.WriteLine(reader.ReadLine());
                }
            }
        }

        [Test]
        public void when_reading_a_file_should_not_be_blocking_delete_operations()
        {
            Console.WriteLine("Creating");

            using (var writer = File.CreateText(name))
            {
                writer.WriteLine("Test");
            }
            using (var readStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Delete))
            {
                File.Delete(name);
            }
        }
        
        [Test]
        public void when_reading_a_file_should_not_be_delete_and_write_operations()
        {
            Console.WriteLine("Creating");
            var writeStream = new FileStream(name, FileMode.Append, FileAccess.Write, FileShare.Read);
            using (var readStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                var reader = new StreamReader(readStream);
                var writer = new StreamWriter(writeStream);
                writer.WriteLine("test");
                writer.Flush();
                Console.WriteLine(reader.ReadLine());
                writer.Dispose();
                File.Delete(name);
            }
        }
        
    }
}