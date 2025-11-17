#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# begin install libgdiplus and dependencies
RUN apt-get update \
    && apt-get install -y \
        apt-transport-https \
        dirmngr \
        gnupg \
        ca-certificates

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev

# begin ttf-mscorefonts-installer
RUN echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections \
	&& apt-get update \
    && apt-get install -y \
        libfontconfig1 \
        xfonts-utils \
		ttf-mscorefonts-installer
# end ttf-mscorefonts-installer
