using GroupDocs.Viewer.Cli.Common.Parameters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupDocs.Viewer.Cli.Tests
{
    /// <summary>
    /// Command-line tests.
    /// </summary>
    public class CommandLineTests : BaseConsoleTests
    {
        [Test]
        public void HelpIsDisplayed()
        {
            string result = CallCliApplication(new string[] { });

            Assert.IsTrue(result.Contains("Usage: groupdocs-viewer [command] [source-file]"));
        }

        [TestCase("-v", true)]
        [TestCase("--verbose", true)]
        [TestCase("", false)]
        public void DisplayedVerbose(string parameter, bool shouldContainVerboseMessages)
        {
            string result = CallCliApplication(new string[] { "get-view-info", "Resources/test.docx", parameter });

            Assert.IsTrue(result.Contains("File info for HTML view:"));
            Assert.IsTrue(result.Contains("[TRACE] Detecting file type by extension.") == shouldContainVerboseMessages);
            Assert.IsTrue(result.Contains("[TRACE] Opening document.") == shouldContainVerboseMessages);
        }

        /// <summary>
        /// Check that parameter help displayed with -h help parameter and with short parameter name (eg. -he (for height parameter)). 
        /// </summary>
        [Test]
        public void HelpForParameterIsDisplayedWhenUsingShortParameterName()
        {
            List<IParameter> parameters = GetAllParameters()
                    .Where(parameter => !string.IsNullOrEmpty(parameter.ShortParameterName))
                    .ToList();

            foreach (IParameter parameter in parameters)
            {
                CheckForShortParameterName(parameter);
            }
        }

        /// <summary>
        /// Check that parameter help displayed with -h help parameter  and with full parameter name (eg. --height (for height parameter)). 
        /// </summary>
        [Test]
        public void HelpForParameterIsDisplayedWhenUsingFullParameterName()
        {
            List<IParameter> parameters = GetAllParameters();

            foreach (IParameter parameter in parameters)
            {
                CheckForFullParameterName(parameter);
            }
        }

        /// <summary>
        /// Check that parameter help exist in help (launch CLI without any arguments). 
        /// </summary>
        [Test]
        public void CheckAllParametersExistInHelpForViewCommand()
        {
            List<IParameter> parameters = GetAllParameters();

            string result = CallCliApplication(new string[] { "view", "-h" });

            foreach (IParameter parameter in parameters)
            {
                Assert.IsTrue(result.Contains(parameter.ParameterName), 
                    "Full parameter name should be displayed in console" + result);
            }
        }

        /// <summary>
        /// Check that all parameters help exist in help for view command. 
        /// </summary>
        [Test]
        public void CheckAllParametersExistInHelpForGetViewCommand()
        {
            List<IParameter> parameters = GetAllParameters();

            string result = CallCliApplication(new string[] { "view", "-h" });

            foreach (IParameter parameter in parameters)
            {
                Assert.IsTrue(result.Contains(parameter.Description),
                    "Full parameter name should be displayed in console" + result);
            }
        }

        /// <summary>
        /// Check that three parameters help exist in help for get-view-info command. 
        /// </summary>
        [Test]
        public void CheckThreeParametersExistInHelpForGetViewInfoCommand()
        {
            List<IParameter> parameters = GetAllParameters().
                Where(x =>
                x.ParameterName == "license-path" ||
                x.ParameterName == "output-format" ||
                x.ParameterName == "verbose").ToList();

            string result = CallCliApplication(new string[] { "get-view-info", "-h" });

            foreach (IParameter parameter in parameters)
            {
                Assert.IsTrue(result.Contains(parameter.Description), "Full parameter name should be displayed in console" + result);
            }
        }

        /// <summary>
        /// Check that other parameters not exist in help for get-view-info command. 
        /// </summary>
        [Test]
        public void CheckOtherParametersNotExistInHelpForGetViewInfoCommand()
        {
            List<IParameter> parameters = GetAllParameters().
                Where(x =>
                x.ParameterName != "license-path" &&
                x.ParameterName != "output-format" &&
                x.ParameterName != "verbose").ToList();

            string result = CallCliApplication(new string[] { "get-view-info", "-h" });

            foreach (IParameter parameter in parameters)
            {
                Assert.IsFalse(result.Contains(parameter.Description), 
                    "Full parameter name should not be displayed in console" + result);
            }
        }

        private void CheckForFullParameterName(IParameter parameter)
        {
            string result = CallCliApplication(
                                new string[] { "view", "Resources\\test.docx", "--" + parameter.ParameterName, "-h" });

            Assert.IsTrue(result.Contains(parameter.GetHelpText()), "CLI should display help for full parameter name! Console result: " + result);
        }

        private void CheckForShortParameterName(IParameter parameter)
        {
            string result = CallCliApplication(
                                new string[] { "view", "Resources\\test.docx", "-" + parameter.ShortParameterName, "-h" });

            Assert.IsTrue(result.Contains(parameter.GetHelpText()), "CLI should display help for short parameter name! Console result: " + result);
        }

        private List<IParameter> GetAllParameters()
        {
            var type = typeof(IParameter);
            var types = Assembly.GetAssembly(typeof(IParameter))
                .GetTypes()
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IParameter).ToList();

            return types;
        }
    }
}