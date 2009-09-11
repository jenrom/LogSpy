using NUnit.Framework;
using System.IO;
using System;
using NUnit.Framework.SyntaxHelpers;
using System.Threading;
using System.Diagnostics;

namespace LogSpy.Tests.Spikes
{
    [TestFixture]
    [Category("Spikes")]
    public class SystemFileWatcherTests
    {
        private string name = "test.txt";
       
        private string copiedFileName = "test2.txt";
        private long fileSize = 10000;

        [SetUp]
        public void before_each()
        {
            try
            {
                File.Delete(name);
                File.Delete(copiedFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        [Test]
        public void Check_how_does_the_file_system_watcher_work_when_creating_file_and_editing_when_watching_at_the_file_size()
        {   
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory);
            bool wasRaised = false;
            watcher.Created += (s, a) =>
                                   {
                                       Console.WriteLine("Created file ");
                                       Assert.That(a.Name, Is.EqualTo(name));
                                       wasRaised = true;
                                   };
            watcher.Changed += (s, a) => Console.WriteLine("Was modified" );
            watcher.NotifyFilter =  NotifyFilters.FileName| NotifyFilters.Size;
            watcher.EnableRaisingEvents = true;
            var stopwatch = new Stopwatch();
            Console.WriteLine("Creating");
            using (var stream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                var writer = new StreamWriter(stream);
                Thread.Sleep(100);
                stopwatch.Start();
                Console.WriteLine("Writing");
                for (int i = 0; i < fileSize; i++)
                {
                    writer.WriteLine("{0}", i);
                }
                Thread.Sleep(100);
                Console.WriteLine("Flushing");
                writer.Flush();
                Thread.Sleep(100);
                Console.WriteLine("Disposing");
                
                writer.Dispose();
                stopwatch.Stop();
            }
            Console.WriteLine("Time elapsed " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("File size " + new FileInfo(name).Length);
            Thread.Sleep(100);
            Assert.That(wasRaised);
            watcher.Dispose();
            
        }

        [Test]
        public void should_not_raise_any_change_event_because_the_file_was_created_without_appending_any_text()
        {
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory);
            bool wasRaised = false;
            watcher.Changed += (s, a) => wasRaised = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;
            using (new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
            }
            Thread.Sleep(100);
            Assert.That(wasRaised, Is.False);
            watcher.Dispose();
        }

        [Test]
        public void should_raise_a_change_event_because_the_file_even_if_only_one_byte_was_appended()
        {
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory);
            bool wasRaised = false;
            watcher.Changed += (s, a) =>
                                   {
                                       Console.WriteLine("Changed");
                                       wasRaised = true;
                                   };
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            using (var stream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                stream.WriteByte(0x0);
            }
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("appending again");
            using (var stream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                stream.WriteByte(0x0);
            }
            Thread.Sleep(100);
            Assert.That(wasRaised);
            watcher.Dispose();
        }

        [Test]
        public void should_notify_about_a_change_in_the_file_in_case_if_the_file_was_copied()
        {
            //Notifies about 2 changes (Probably one alocation, second finished coping)
            bool wasChanged = false;
            using (var writer = File.CreateText(name))
            {
                for (int i = 0; i < fileSize; i++)
                {
                    writer.WriteLine("{0}", i);
                }
            }
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory, copiedFileName);
            watcher.Created += (s, a) => Console.WriteLine("Created file");
            watcher.Changed += (s, a) =>
                                   {
                                       wasChanged = true;
                                       Console.WriteLine("Changed");
                                       Console.WriteLine(new FileInfo(copiedFileName).Length);
                                   };
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Coping file");
            File.Copy(name, copiedFileName);
            Console.WriteLine(new FileInfo(copiedFileName).Length);
            Thread.Sleep(1000);
            Assert.IsTrue(wasChanged);
            watcher.Dispose();
        }


        [Test]
        public void should_not_notify_about_a_change_in_the_file_in_case_if_the_file_was_moved()
        {
            bool wasChanged = false;
            using (var writer = File.CreateText(name))
            {
                for (int i = 0; i < fileSize; i++)
                {
                    writer.WriteLine("{0}", i);
                }
            }
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory, copiedFileName);
            watcher.Created += (s, a) => Console.WriteLine("Created file");
            watcher.Changed += (s, a) => { wasChanged = true;};
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite| NotifyFilters.LastAccess;
            watcher.EnableRaisingEvents = true;
            File.Move(name, copiedFileName);
            Thread.Sleep(1000);
            Assert.IsFalse(wasChanged);
            watcher.Dispose();
        }

        [Test]
        public void should_notify_about_a_file_rename_in_the_file_in_case_if_the_file_was_moved()
        {
            bool wasRenamed = false;
            using (var writer = File.CreateText(name))
            {
                for (int i = 0; i < fileSize; i++)
                {
                    writer.WriteLine("{0}", i);
                }
            }
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory, copiedFileName);
            watcher.Created += (s, a) => Console.WriteLine("Created file");
            watcher.Renamed += (s, a) =>
                                   {
                                       Console.WriteLine("File was renamed");
                                       wasRenamed = true;
                                   };
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            watcher.EnableRaisingEvents = true;
            File.Move(name, copiedFileName);
            Thread.Sleep(1000);
            Assert.IsTrue(wasRenamed);
            watcher.Dispose();
        }

        [Test]
        public void Check_how_does_the_file_system_watcher_work_when_editing_a_file_in_chunks()
        {
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory);
            watcher.Changed += (s, a) => Console.WriteLine("Was modified");
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Creating");
            using (var stream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 6000))
            {
                var writer = new StreamWriter(stream);
                Thread.Sleep(100);
                Console.WriteLine("Writing");
                for (int i = 0; i < fileSize / 4; i++)
                {
                    writer.WriteLine("{0}", i);
                }
                Thread.Sleep(100);
                Console.WriteLine("File size " + new FileInfo(name).Length);
                for (int i = 0; i < fileSize / 4; i++)
                {
                    writer.WriteLine("{0}", i);
                }
                Thread.Sleep(100);
                Console.WriteLine("File size " + new FileInfo(name).Length);
                for (int i = 0; i < fileSize / 4; i++)
                {
                    writer.WriteLine("{0}", i);
                }
                Thread.Sleep(100);
                Console.WriteLine("File size " + new FileInfo(name).Length);
                for (int i = 0; i < fileSize /4; i++)
                {
                    writer.WriteLine("{0}", i);
                }
                Thread.Sleep(100);
                Console.WriteLine("File size " + new FileInfo(name).Length);
                Console.WriteLine("Flushing");
                writer.Flush();
                Thread.Sleep(100);
                Console.WriteLine("Disposing");

                writer.Dispose();
                
            }
            Console.WriteLine("File size " + new FileInfo(name).Length);
            Thread.Sleep(100);
            watcher.Dispose();
        }

        [Test]
        public void Check_how_does_the_file_system_watcher_work_when_editing_file()
        {
            var watcher = new FileSystemWatcher(Environment.CurrentDirectory, name);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;
            bool wasRaised = false;
            watcher.Changed += (s, a) =>
                                   {
                                       Console.WriteLine("File has been changed");
                                       Assert.That(a.Name, Is.EqualTo(name));
                                       wasRaised = true;
                                   };
            Console.WriteLine("Creating");
            using(var writeStream = File.Open(name, FileMode.Append, FileAccess.Write))
            {
                var writer = new StreamWriter(writeStream);
                writer.AutoFlush = true;
                Thread.Sleep(100);
                Console.WriteLine("Writing");
                writer.WriteLine("test");
                Thread.Sleep(100);
                Console.WriteLine("Flushing");
                writer.Flush();
                Thread.Sleep(100);
                Console.WriteLine("Disposing");
                writer.Dispose();
                Thread.Sleep(100);
                
            }
            Assert.That(wasRaised);
            watcher.Dispose();
        }

    }
}