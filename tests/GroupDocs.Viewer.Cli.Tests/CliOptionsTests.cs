using GroupDocs.Viewer.UI.Core;
using Xunit;

namespace GroupDocs.Viewer.Cli.Tests;

public class CliOptionsTests
{
    [Fact]
    public void Parse_NoArgs_DefaultsToCurrentDirectory()
    {
        var options = CliOptions.Parse([]);

        Assert.Equal(Directory.GetCurrentDirectory(), options.StoragePath);
        Assert.False(options.IsFileMode);
        Assert.Null(options.FileName);
        Assert.Equal(8080, options.Port);
        Assert.Equal("localhost", options.Bind);
        Assert.False(options.NoOpen);
        Assert.Equal(ViewerType.HtmlWithEmbeddedResources, options.ViewerType);
    }

    [Fact]
    public void Parse_Help_SetsShowHelp()
    {
        var options = CliOptions.Parse(["--help"]);
        Assert.True(options.ShowHelp);
    }

    [Theory]
    [InlineData("-h")]
    [InlineData("-?")]
    [InlineData("/?")]
    public void Parse_HelpShortForms_SetsShowHelp(string arg)
    {
        var options = CliOptions.Parse([arg]);
        Assert.True(options.ShowHelp);
    }

    [Fact]
    public void Parse_Version_SetsShowVersion()
    {
        var options = CliOptions.Parse(["--version"]);
        Assert.True(options.ShowVersion);
    }

    [Fact]
    public void Parse_Port_SetsPort()
    {
        var options = CliOptions.Parse(["--port", "9090"]);
        Assert.Equal(9090, options.Port);
    }

    [Fact]
    public void Parse_PortShort_SetsPort()
    {
        var options = CliOptions.Parse(["-p", "3000"]);
        Assert.Equal(3000, options.Port);
    }

    [Fact]
    public void Parse_Bind_SetsBind()
    {
        var options = CliOptions.Parse(["--bind", "0.0.0.0"]);
        Assert.Equal("0.0.0.0", options.Bind);
    }

    [Fact]
    public void Parse_NoOpen_SetsNoOpen()
    {
        var options = CliOptions.Parse(["--no-open"]);
        Assert.True(options.NoOpen);
    }

    [Fact]
    public void Parse_Verbose_SetsVerbose()
    {
        var options = CliOptions.Parse(["--verbose"]);
        Assert.True(options.Verbose);
    }

    [Fact]
    public void Parse_VerboseShort_SetsVerbose()
    {
        var options = CliOptions.Parse(["-v"]);
        Assert.True(options.Verbose);
    }

    [Theory]
    [InlineData("Html", ViewerType.HtmlWithEmbeddedResources)]
    [InlineData("html", ViewerType.HtmlWithEmbeddedResources)]
    [InlineData("HtmlExt", ViewerType.HtmlWithExternalResources)]
    [InlineData("Png", ViewerType.Png)]
    [InlineData("Jpg", ViewerType.Jpg)]
    public void Parse_ViewerType_SetsViewerType(string input, ViewerType expected)
    {
        var options = CliOptions.Parse(["--viewer-type", input]);
        Assert.Equal(expected, options.ViewerType);
    }

    [Fact]
    public void Parse_InvalidViewerType_Throws()
    {
        Assert.Throws<ArgumentException>(() => CliOptions.Parse(["--viewer-type", "invalid"]));
    }

    [Fact]
    public void Parse_DirectoryPath_SetsDirectoryMode()
    {
        var tempDir = Path.GetTempPath();
        var options = CliOptions.Parse([tempDir]);

        Assert.Equal(Path.GetFullPath(tempDir), options.StoragePath);
        Assert.False(options.IsFileMode);
    }

    [Fact]
    public void Parse_FilePath_SetsFileMode()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var options = CliOptions.Parse([tempFile]);

            Assert.Equal(Path.GetDirectoryName(Path.GetFullPath(tempFile)), options.StoragePath);
            Assert.True(options.IsFileMode);
            Assert.Equal(Path.GetFileName(tempFile), options.FileName);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void Parse_NonexistentPath_Throws()
    {
        Assert.Throws<ArgumentException>(() => CliOptions.Parse(["/nonexistent/path/xyz"]));
    }

    [Fact]
    public void Parse_UnknownOption_Throws()
    {
        Assert.Throws<ArgumentException>(() => CliOptions.Parse(["--unknown"]));
    }

    [Fact]
    public void Parse_PortWithoutValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => CliOptions.Parse(["--port"]));
    }

    [Fact]
    public void Parse_PortWithInvalidValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => CliOptions.Parse(["--port", "abc"]));
    }

    [Fact]
    public void Parse_MultipleOptions_AllApplied()
    {
        var options = CliOptions.Parse(["--port", "9090", "--bind", "0.0.0.0", "--no-open", "-v"]);

        Assert.Equal(9090, options.Port);
        Assert.Equal("0.0.0.0", options.Bind);
        Assert.True(options.NoOpen);
        Assert.True(options.Verbose);
    }
}
