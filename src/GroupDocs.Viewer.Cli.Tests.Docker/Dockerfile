#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic
WORKDIR /app

# begin install libgdiplus and dependencies
RUN apt-get update \
    && apt-get install -y \
        apt-transport-https \
        dirmngr \
        gnupg \
        ca-certificates

RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
    && echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" >> /etc/apt/sources.list.d/mono-official-stable.list

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
