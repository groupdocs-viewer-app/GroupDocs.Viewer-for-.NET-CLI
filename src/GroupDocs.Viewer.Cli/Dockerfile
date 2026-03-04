FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/GroupDocs.Viewer.Cli/GroupDocs.Viewer.Cli.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN sed -i 's/^Components: main$/Components: main contrib/' /etc/apt/sources.list.d/debian.sources \
    && apt-get update \
    && apt-get install -y libc6-dev libgdiplus libx11-dev \
    && apt-get install -y libfontconfig1 xfonts-utils \
    && echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections \
    && apt-get install -y ttf-mscorefonts-installer \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "groupdocs-viewer.dll", "--bind", "0.0.0.0", "--no-open"]
