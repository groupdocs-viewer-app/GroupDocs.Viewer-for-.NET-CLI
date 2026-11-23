# The .NET 10 images are based on Ubuntu, so the Microsoft core fonts come from the Debian package.
# They're installed in a separate stage, so cabextract, wget and dpkg never reach the runtime image:
# only the font files are copied out.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS fonts

RUN apt-get update && apt-get install -y --no-install-recommends \
    fontconfig \
    cabextract \
    xfonts-utils \
    ca-certificates \
    wget \
 && rm -rf /var/lib/apt/lists/*

RUN echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections \
 && wget -q -O /tmp/mscorefonts.deb \
    https://deb.debian.org/debian/pool/contrib/m/msttcorefonts/ttf-mscorefonts-installer_3.8.1_all.deb \
 && dpkg -i /tmp/mscorefonts.deb \
 && rm /tmp/mscorefonts.deb


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/GroupDocs.Viewer.Cli/GroupDocs.Viewer.Cli.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# fontconfig is required by GroupDocs.Viewer at runtime
RUN apt-get update && apt-get install -y --no-install-recommends \
    fontconfig \
 && rm -rf /var/lib/apt/lists/*

COPY --from=fonts /usr/share/fonts/truetype/msttcorefonts /usr/share/fonts/truetype/msttcorefonts
RUN fc-cache -f

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "groupdocs-viewer.dll", "--bind", "0.0.0.0", "--no-open"]
