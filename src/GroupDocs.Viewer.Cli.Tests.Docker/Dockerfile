#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

# Dependencies
RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt update && apt install -y \
    libgdiplus \
    libc6-dev \
    libx11-dev \
    fontconfig \
    ttf-mscorefonts-installer \
    fonts-wqy-zenhei \
    dirmngr \
    && rm -rf /var/lib/apt/lists/*
