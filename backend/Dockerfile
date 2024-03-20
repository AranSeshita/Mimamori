# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/Mimamori.csproj", "backend/"]
RUN dotnet restore "backend/Mimamori.csproj"
COPY . .
WORKDIR "/src/backend"
RUN dotnet build "Mimamori.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Mimamori.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mimamori.dll"]