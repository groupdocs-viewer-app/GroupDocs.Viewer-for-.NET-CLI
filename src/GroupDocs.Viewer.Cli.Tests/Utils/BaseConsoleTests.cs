using GroupDocs.Viewer.Cli.Tests.Utils;
using GroupDocs.Viewer.Cli.Utils;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace GroupDocs.Viewer.Cli.Tests
{
    public class BaseConsoleTests
    {
        protected static object lockObject = new object();
        protected ConsoleOutput ConsoleOutput;

        [SetUp]
        public void TestPreClean()
        {
            // Clear console 
            if (ConsoleOutput != null)
            {
                ConsoleOutput.Dispose();
            }

            // Clear console 
            ConsoleOutput = new ConsoleOutput();
            Reporter.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            Reporter.Reset();
        }

        /// <summary>
        /// Call CLI application and get console output.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Console output.</returns>
        protected string CallCliApplication(string[] args)
        {
            string result = string.Empty;

            Assembly assembly = Assembly.LoadFrom("groupdocs-viewer.dll");

            lock (lockObject)
            {
                assembly.EntryPoint.Invoke(null, new object[] { args });
                result = ConsoleOutput.GetOuput() + ConsoleOutput.GetError();
            }

            return result;
        }

        protected void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
