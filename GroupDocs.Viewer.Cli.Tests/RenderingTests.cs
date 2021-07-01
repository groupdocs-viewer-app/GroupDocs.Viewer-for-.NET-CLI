using NUnit.Framework;
using System;
using System.IO;

namespace GroupDocs.Viewer.Cli.Tests
{
    public class RenderingTests : BaseConsoleTests
    {
        const string resourcesCssMask = "p_{0}_s.css";

        [TestCase("sample.cdr", new int[] { 1 })]
        [TestCase("sample.cmx", new int[] { 1 })]
        [TestCase("sample.docx", new int[] { 1, 2 })]
        [TestCase("sample.xlsx", new int[] { 1, 2 })]
        public void RenderDocumentToPng(string document, int[] pages)
        {
            string outputDocumentPage1 = "output_1.png";
            string outputDocumentPage2 = "output_2.png";

            DeleteFileIfExists(outputDocumentPage1);
            DeleteFileIfExists(outputDocumentPage2);

            string result = CallCliApplication(new string[] { "view", "Resources/" + document, "-v", "-f", "Png", "-p",
                string.Join(",", pages) });

            Assert.IsFalse(result.Contains("[ERROR]"), "Should no errors when rendering");

            if (Array.IndexOf(pages, 1) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage1));
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage2));
            }

            if (Array.IndexOf(pages, 1) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage1);
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage2);
            }
        }

        [TestCase("sample.cdr", new int[] { 1 })]
        [TestCase("sample.cmx", new int[] { 1 })]
        [TestCase("sample.docx", new int[] { 1, 2 })]
        [TestCase("sample.xlsx", new int[] { 1, 2 })]
        public void RenderDocumentToPdf(string document, int[] pages)
        {
            string outputDocumentPage = "output.pdf";

            DeleteFileIfExists(outputDocumentPage);

            string result = CallCliApplication(new string[] { "view", "Resources/" + document, "-v", "-f", "Pdf", "-p",
                string.Join(",", pages) });

            Assert.IsFalse(result.Contains("[ERROR]"), "Should no errors when rendering");

            if (Array.IndexOf(pages, 1) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage));
            }

            if (Array.IndexOf(pages, 1) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage);
            }
        }

        [TestCase("sample.cdr", new int[] { 1 })]
        [TestCase("sample.cmx", new int[] { 1 })]
        [TestCase("sample.docx", new int[] { 1, 2 })]
        [TestCase("sample.xlsx", new int[] { 1, 2 })]
        public void RenderDocumentToJpg(string document, int[] pages)
        {
            string outputDocumentPage1 = "output_1.jpg";
            string outputDocumentPage2 = "output_2.jpg";

            DeleteFileIfExists(outputDocumentPage1);
            DeleteFileIfExists(outputDocumentPage2);

            string result = CallCliApplication(new string[] { "view", "Resources/" + document, "-v", "-f", "Jpg", "-p",
                string.Join(",", pages) });

            Assert.IsFalse(result.Contains("[ERROR]"), "Should no errors when rendering");

            if (Array.IndexOf(pages, 1) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage1));
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage2));
            }

            if (Array.IndexOf(pages, 1) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage1);
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage2);
            }
        }

        [TestCase("sample.cdr", new int[] { 1 })]
        [TestCase("sample.cmx", new int[] { 1 })]
        [TestCase("sample.docx", new int[] { 1, 2 })]
        [TestCase("sample.xlsx", new int[] { 1, 2 })]
        public void RenderDocumentToHtml(string document, int[] pages)
        {
            string outputDocumentPage1 = "output_1.html";
            string outputDocumentPage2 = "output_2.html";

            DeleteFileIfExists(outputDocumentPage1);
            DeleteFileIfExists(outputDocumentPage2);

            string result = CallCliApplication(new string[] { "view", "Resources/" + document, "-v", "-f", "Html", "-p",
                string.Join(",", pages) });

            Assert.IsFalse(result.Contains("[ERROR]"), "Should no errors when rendering");

            if (Array.IndexOf(pages, 1) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage1));
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage2));
            }

            if (Array.IndexOf(pages, 1) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage1);
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage2);
            }
        }

        [TestCase("sample.cdr", new int[] { 1 })]
        [TestCase("sample.cmx", new int[] { 1 })]
        [TestCase("sample.docx", new int[] { 1, 2})]
        [TestCase("sample.xlsx", new int[] { 1, 2 })]
        public void RenderDocumentToExternalHtml(string document, int[] pages)
        {
            string outputDocumentPage1 = "output_1.html";
            string outputDocumentPage2 = "output_2.html";

            // Delete already created resources for pages if exist
            DeleteResourcesIfExistsForPage(1);
            DeleteResourcesIfExistsForPage(2);

            DeleteFileIfExists(outputDocumentPage1);
            DeleteFileIfExists(outputDocumentPage2);

            string result = CallCliApplication(new string[] { "view", "Resources/" + document, "-v", "-f", "HtmlExt", "-p",
                string.Join(",", pages) });

            Assert.IsFalse(result.Contains("[ERROR]"), "Should no errors when rendering");

            if (Array.IndexOf(pages, 1) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage1));
                CheckResourcesFilesExistForPage(1);
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                Assert.IsTrue(File.Exists(outputDocumentPage2));
                CheckResourcesFilesExistForPage(2);
            }
   
            if (Array.IndexOf(pages, 1) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage1);
            }

            if (Array.IndexOf(pages, 2) != -1)
            {
                FileHasNonZeroSize(outputDocumentPage2);
            }
        }

        private void CheckResourcesFilesExistForPage(int pageNumber)
        {
            Assert.IsTrue(File.Exists(string.Format(resourcesCssMask, pageNumber)));
        }

        private void DeleteResourcesIfExistsForPage(int pageNumber)
        {
            DeleteFileIfExists(string.Format(resourcesCssMask,pageNumber));
        }

        private void FileHasNonZeroSize(string outputDocumentPage1)
        {
            long length = new FileInfo(outputDocumentPage1).Length;

            Assert.IsTrue(length > 0);
        }
    }
}
