using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using NUnit.Framework;

namespace GroupDocs.Viewer.Cli.Tests.Docker
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            RunTest();
            Cleanup();
        }

        /// <summary>
        /// Run tests.
        /// </summary>
        private static void RunTest()
        {
            Assembly testAssembly = Assembly.LoadFrom("GroupDocs.Viewer.Cli.Tests.dll");
            TextWriter mainConsole = Console.Out;

            foreach (Type type in testAssembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(BaseConsoleTests)))
                {
                    // If type is derived from BaseConsoleTests - create main tests object
                    var instance = Activator.CreateInstance(type);

                    foreach (MethodInfo methodInfo in type.GetMethods())
                    {
                        var attributes = methodInfo.GetCustomAttributes(true);

                        foreach (var attr in attributes)
                        {
                            // Call [SetUp]  method before each test
                            (instance as BaseConsoleTests).TestPreClean();

                            var methodName = methodInfo.Name;

                            if (attr.ToString() == "NUnit.Framework.TestAttribute")
                            {
                                mainConsole.WriteLine($"Running {methodName}");
                                methodInfo.Invoke(instance, null);
                            }

                            if (attr.ToString() == "NUnit.Framework.TestCaseAttribute")
                            {
                                TestCaseAttribute testCaseAttribute = (TestCaseAttribute)attr;
                                string argumentsInfoString = GenerateArgumentsInfoString(testCaseAttribute);

                                mainConsole.WriteLine($"Running {methodName} ({argumentsInfoString}))");
                                methodInfo.Invoke(instance, testCaseAttribute.Arguments);
                            }

                            // Call [TearDown] method after each test
                            (instance as BaseConsoleTests).TearDown();
                        }
                    }
                }
            }

            mainConsole.WriteLine("All tests passed!");
        }

        /// <summary>
        /// Generate arguments string like Method("1","true") for better understanding what test called.
        /// </summary>
        /// <param name="testCaseAttribute">Testcase attribute to generate info string.</param>
        /// <returns>Arguments string.</returns>
        private static string GenerateArgumentsInfoString(TestCaseAttribute testCaseAttribute)
        {
            return string.Join(",", testCaseAttribute.Arguments.Select(x => string.Format("\"{0}\"", GenerateArgumentString(x))));
        }

        /// <summary>
        /// Generate formatted string for argument object.
        /// </summary>
        /// <param name="argument">argument object.</param>
        /// <returns>Formatted string.</returns>
        private static string GenerateArgumentString(object argument)
        {
            if (argument is System.Int32[])
            {
                return string.Join(",", (System.Int32[])argument);
            }

            return argument.ToString();
        }


        /// <summary>
        /// Inititialization.
        /// </summary>
        private static void Init()
        {
            // Set current directory to bin/Debug as NUnit 3.0 doesn't change current directory.
            // see - https://stackoverflow.com/a/36223859
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Cleanup generated files after all tests.
        /// </summary>
        private static void Cleanup()
        {
            string[] ext = new string[] { "*.woff", "*.html", "*.png", "*.jpg", "*.css", "*.svg", "*.pdf" };

            foreach (string extensionToDelete in ext)
            {
                string[] extracted = Directory.GetFiles(".", extensionToDelete);

                foreach (string file in extracted)
                {
                    File.Delete(file);
                }
            }
        }
    }
}