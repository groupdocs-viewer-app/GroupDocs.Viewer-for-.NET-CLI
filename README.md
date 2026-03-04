# GroupDocs.Viewer CLI

[![NuGet](https://img.shields.io/nuget/v/groupdocs.viewer-cli?label=nuget)](https://www.nuget.org/packages/groupdocs.viewer-cli)
[![NuGet Downloads](https://img.shields.io/nuget/dt/groupdocs.viewer-cli)](https://www.nuget.org/packages/groupdocs.viewer-cli)

View documents in the browser from your terminal. Powered by [GroupDocs.Viewer for .NET](https://products.groupdocs.com/viewer/net) — supports **180+ file formats** including PDF, Word, Excel, PowerPoint, images, CAD drawings, archives, ebooks, and source code.

```
groupdocs-viewer .
```

```
   ____                    ____
  / ___|_ __ ___  _   _ _ |  _ \  ___   ___ ___
 | |  _| '__/ _ \| | | | '| | | |/ _ \ / __/ __|
 | |_| | | | (_) | |_| | ,| |_| | (_) | (__\__ \
  \____|_|  \___/ \__,_|_|| ____/ \___/ \___|___/
                          |_|
  V I E W E R  26.3.0

  Serving files from: /home/user/documents
  Listening on: http://localhost:8080/

  Press Ctrl+C to stop the server.
```

## Install

Requires [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) or later.

```bash
dotnet tool install --global GroupDocs.Viewer-CLI
```

## Quick Start

View all files in the current directory:

```bash
groupdocs-viewer
```

View files in a specific directory:

```bash
groupdocs-viewer ~/documents
```

Open a single file directly (file browser disabled):

```bash
groupdocs-viewer report.pdf
```

The browser opens automatically at `http://localhost:8080/`.

## Options

```
Usage: groupdocs-viewer [path] [options]

Arguments:
  path                       File or directory to serve (default: current directory)

Options:
  -p, --port <port>          Port to listen on (default: 8080)
  -b, --bind <address>       Bind address (default: localhost)
      --no-open              Don't auto-open browser
  -l, --license-path <path>  Path to GroupDocs license file
  -t, --viewer-type <type>   Viewer type: Html, HtmlExt, Png, Jpg (default: Html)
  -v, --verbose              Enable verbose logging
      --version              Show version info
  -h, --help                 Show help
```

### Examples

Serve on a custom port:

```bash
groupdocs-viewer --port 3000
```

Use image-based rendering:

```bash
groupdocs-viewer --viewer-type Png
```

Bind to all interfaces (e.g. for Docker or LAN access):

```bash
groupdocs-viewer --bind 0.0.0.0
```

## Licensing

Without a license the viewer runs in evaluation mode — only the first 2 pages of each document are rendered.

You can request a **free 30-day temporary license** at [purchase.groupdocs.com/temporary-license](https://purchase.groupdocs.com/temporary-license).

The license is resolved in the following order:

1. `--license-path` option
2. `GROUPDOCS_LIC_PATH` environment variable
3. `GroupDocs.Viewer.lic` in the current directory
4. `GroupDocs.Viewer.Product.Family.lic` in the current directory

```bash
# Option
groupdocs-viewer --license-path /path/to/GroupDocs.Viewer.lic

# Environment variable
export GROUPDOCS_LIC_PATH=/path/to/GroupDocs.Viewer.lic
groupdocs-viewer
```

## Docker

```bash
docker build -t groupdocs-viewer -f src/GroupDocs.Viewer.Cli/Dockerfile .
docker run -p 8080:8080 -v /path/to/files:/files groupdocs-viewer /files
```

## Linux Dependencies

On Linux, install the following packages:

```bash
sudo apt-get update
sudo apt-get install -y libc6-dev libgdiplus libx11-dev libfontconfig1 ttf-mscorefonts-installer
```

## Supported Formats

| Category | Formats |
|---|---|
| PDF & XPS | PDF, XPS, OXPS, TEX |
| Microsoft Word | DOC, DOCX, DOCM, DOT, DOTX, DOTM, RTF, TXT |
| Microsoft Excel | XLS, XLSX, XLSM, XLSB, CSV, TSV, XLTX |
| Microsoft PowerPoint | PPT, PPTX, PPS, PPSX, PPTM, POTX, POT |
| Microsoft Visio | VSD, VSDX, VSS, VST, VSTX, VSX, VDW, VDX |
| Microsoft Project | MPP, MPT, MPX |
| Microsoft Outlook | MSG, EML, EMLX, PST, OST |
| OpenDocument | ODT, ODS, ODP, OTT, OTS, OTP, FODG, FODT, FODP |
| Images | TIFF, JPG, PNG, GIF, BMP, PSD, SVG, WEBP, ICO, TGA, JP2, DNG, DJVU |
| CAD & 3D | DXF, DWG, DWT, STL, IFC, DWF, DGN, OBJ, CF2, PLT, HPG |
| Archives | ZIP, TAR, RAR, 7Z, GZ, BZ2, XZ |
| Ebooks | EPUB, MOBI |
| Web & Markup | HTML, MHT, MHTML, XML, CHM |
| Source Code | CS, VB, Java, JS, TS, PY, PHP, SQL, CSS, JSON, and more |

See the full list at [docs.groupdocs.com/viewer/net/supported-document-formats](https://docs.groupdocs.com/viewer/net/supported-document-formats/).

## Resources

- [GroupDocs.Viewer for .NET](https://products.groupdocs.com/viewer/net) — product page
- [Documentation](https://docs.groupdocs.com/viewer/net/) — developer guide
- [Free Support](https://forum.groupdocs.com/c/viewer) — community forum
- [Temporary License](https://purchase.groupdocs.com/temporary-license) — free 30-day trial
