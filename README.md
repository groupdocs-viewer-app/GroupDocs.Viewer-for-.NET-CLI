# CLI for GroupDocs.Viewer for .NET

![Nuget](https://img.shields.io/nuget/v/groupdocs.viewer-cli)
![Nuget](https://img.shields.io/nuget/dt/groupdocs.viewer-cli)


CLI - Command Line Interface for [GroupDocs.Viewer for .NET](https://products.groupdocs.com/viewer/net) document viewer and automation API.

## How to install

GroupDocs.Viewer CLI is a dotnet tool. To start using the CLI you'll need the .NET 8 runtime and GroupDocs.Viewer CLI.

1. Install the .NET 8 runtime following the [installation guide](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Install the dotnet tool by running `dotnet tool install --global groupdocs.viewer-cli`
3. You can run GroupDocs.Viewer.CLI by using command `groupdocs-viewer`

## Example usage

Type `view` command and source filename to convert the file to HTML and place the output in the current directory:

```bash
groupdocs-viewer view source.docx
```

Set `output-format` parameter value to `PDF` to convert document to PDF:

```bash
groupdocs-viewer view source.docx --output-format PDF
```

Set `pages` parameter value to `1,2,3` to convert the first three pages of the document:

```bash
groupdocs-viewer view source.docx --output-format pdf --pages 1,2,3
```

The `--help` or `view --help` option provides more detail about each parameter. \
The `--version` option provides information about CLI version in use.

## Commands

* `view` converts file to specified `output-format`. The default value is `HTML`.

* `get-view-info` retrieves view details for a specified file that includes list of pages and source file-type.

## Parameters

### Parameters for "view" command

* `--file-type`: Source document file type e.g. `DOCX`.

* `--password` [short: `-pwd`]: Password to open password-protected file.

* `--encoding` [short: `-enc`]: Source document encoding e.g `UTF-8`.

* `--pages` [short: `-p`]: Comma-separated page numbers to convert e.g `1,2,3`.

* `--output` [short `-o`]: Output file path when converting to HTML, PNG, and JPG. Or output filename when converting to PDF.

* `--resource-filepath-template`: Resource filepath template when converting to HTML with external resources e.g. `p_{0}_{1}`.

* `--resource-url-format`: Resource URL format for HTML with external resources.

* `--height`: Output image height.

* `--width`: Output image width.

* `--max-height`: Max height for output image.

* `--max-width`: Max width for output image.

### Parameters for "view" and "get-view-info" commands

* `--license-path`: Path to license file.

* `--output-format` [short: `-f`]: Output format, supported values are `Html`, `HtmlExt`, `Png`, `Jpg`, and `Pdf`.

* `--verbose` [short `-v`]: Enable detailed logging to console.

* `--save-attachments`: Save attachments on disk if selected file support it.

* `--attachments-list`: Get attachments list for selected file if it supported.


## Setting the license

Without a license the tool will work in trial mode so you can convert only first two pages of a document see [Evaluation Limitations and Licensing of GroupDocs.Viewer](https://docs.groupdocs.com/viewer/net/evaluation-limitations-and-licensing-of-groupdocs-viewer/) for more details. A temporary license can be requested at [Get a Temporary License](https://purchase.groupdocs.com/temporary-license).

The license can be set with `--license-path` parameter:

```bash
groupdocs-viewer view source.docx --license-path c:\\licenses\\GroupDocs.Viewer.lic
```

Also, you can set path to the license file in `GROUPDOCS_VIEWER_LICENSE_PATH` environment variable.

## Linux dependencies

To run on Linux, install the following package dependencies:

* `libc6-dev`
* `libgdiplus`
* `libx11-dev`
* `libfontconfig1`
* `ttf-mscorefonts-installer`

Example for Debian/Ubuntu (22.04+):

```bash
sudo apt-get update
sudo apt-get install -y libc6-dev libgdiplus libx11-dev libfontconfig1 ttf-mscorefonts-installer
```
