using NUnit.Framework;
using System;
using System.IO;

namespace GroupDocs.Viewer.Cli.Tests
{
    /// <summary>
    /// Working with attachments tests.
    /// </summary>
    public class AttachmentsTests : BaseConsoleTests
    {
        #region Attachments list

        /// <summary>
        /// Check that get attachments list working for MSG file.
        /// </summary>
        [Test]
        public void CheckGetAttachmentsListWorkingForMsg()
        {
            string result = CallCliApplication(new string[] { "view", @"Resources\sample.msg", "--attachments-list" });

            Assert.IsTrue(result.Contains("sample.cdr"));
        }

        /// <summary>
        /// Check that get attachments list working for ZIP file.
        /// </summary>
        [Test]
        public void CheckGetAttachmentsListWorkingForZip()
        {
            string result = CallCliApplication(new string[] { "view", @"Resources\with_folders.zip", "--attachments-list" });

            Assert.IsTrue(result.Contains("ThirdFolderWithItems"));
            Assert.IsTrue(result.Contains("file (12).txt"));
            Assert.IsTrue(result.Contains("file (11).txt"));
        }

        /// <summary>
        /// Check that get attachments list working for 7-ZIP file.
        /// </summary>
        [Test]
        public void CheckGetAttachmentsListWorkingFor7Zip()
        {
            string result = CallCliApplication(new string[] { "view", @"Resources\7Zip.7z", "--attachments-list" });

            Assert.IsTrue(result.Contains("ThirdFolderWithItems"));
            Assert.IsTrue(result.Contains("file (12).txt"));
            Assert.IsTrue(result.Contains("file (11).txt"));
        }

        #endregion Attachments list

        #region Save attachments

        /// <summary>
        /// Check that save attachments by list working for MSG file.
        /// </summary>
        [Test]
        public void CheckSaveAttachmentsWorkingForMsg()
        {
            DeleteFileIfExists("sample.cdr");
            string result = CallCliApplication(new string[] { "view", @"Resources\sample.msg", "--save-attachments", "07c90715-040d-09c8-100a-c6040e05c507" });

            VerifyAttachmentSaving(result, "07c90715-040d-09c8-100a-c6040e05c507", "sample.cdr");
        }

        /// <summary>
        /// Check that save attachments by list working for ZIP file.
        /// </summary>
        [Test]
        public void CheckSaveAttachmentsWorkingForZip()
        {
            DeleteFileIfExists("sample-inside-folder.txt");
            DeleteFileIfExists("file (12).txt");
            DeleteFileIfExists("file (11).txt");

            string result = CallCliApplication(new string[] { "view", @"Resources\with_folders.zip",
                "--save-attachments",
                "'ThirdFolderWithItems/sample-inside-folder.txt','file (12).txt','file (11).txt'" });

            VerifyAttachmentSaving(result, "ThirdFolderWithItems/sample-inside-folder.txt");
            VerifyAttachmentSaving(result, "file (12).txt");
            VerifyAttachmentSaving(result, "file (11).txt");
        }

        /// <summary>
        /// Check that error message displayed for 7-Zip (7-Zip does not support attachments saving, only get attachment list).
        /// </summary>
        [Test]
        public void CheckErrorMessageDisplayedForFor7Zip()
        {
            string result = CallCliApplication(new string[] { "view", @"Resources\7Zip.7z", "--save-attachments", "'file (12).txt'" });

            Assert.IsTrue(result.Contains("Attachments saving is not supported for 7-Zip format"));
        }

        /// <summary>
        /// Show error message when attachments ids are not supplied with --save-attachments parameter.
        /// </summary>
        [Test]
        public void ShowErrorWhenNoAttachmentsIds()
        {
            string errorString = "Attachments Ids are not supplied, please supply attachments id separated by, in quotes: \'test.txt\', 'test2.txt\'";

            string result = CallCliApplication(new string[] { "view", @"Resources\with_folders.zip",
                "--save-attachments"});

            Assert.IsTrue(result.Contains(errorString));
        }

        private void VerifyAttachmentSaving(string result, string attachmentId, string fileName = "")
        {
            string resultFileName = attachmentId;

            // If attachment inside in email format it will be identified by GUID, not filename.
            if (!string.IsNullOrEmpty(fileName))
            {
                resultFileName = fileName;
            }

            Assert.IsTrue(result.Contains($"Saving attachment with ID '{attachmentId}'."), "Saving attachment message should exist");
            Assert.IsTrue(File.Exists(Path.GetFileName(resultFileName)), "The file should be saved on disk");
        }


        #endregion
    }
}
