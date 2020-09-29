#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["cms.ar.xarchitecture.de/cms.ar.xarchitecture.de.csproj", "cms.ar.xarchitecture.de/"]
RUN dotnet restore "cms.ar.xarchitecture.de/cms.ar.xarchitecture.de.csproj"
COPY . .
WORKDIR "/src/cms.ar.xarchitecture.de"
RUN dotnet build "cms.ar.xarchitecture.de.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cms.ar.xarchitecture.de.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cms.ar.xarchitecture.de.dll"]

USER 1003:1004

ENV DB_SERVER=127.0.0.1
ENV DB_PORT=9000
ENV DB_NAME=default
ENV DB_USER=default
ENV DB_PASSWORD=default

VOLUME /app/wwwroot/content/assets
VOLUME /app/wwwroot/content/worldmaps
VOLUME /app/wwwroot/content/marker
VOLUME /app/wwwroot/content/thumbnails
