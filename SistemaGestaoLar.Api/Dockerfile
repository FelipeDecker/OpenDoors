# syntax=docker/dockerfile:1

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files first for better layer caching
COPY SistemaGestaoLar.Api/SistemaGestaoLar.Api.csproj SistemaGestaoLar.Api/
RUN dotnet restore SistemaGestaoLar.Api/SistemaGestaoLar.Api.csproj

# Copy the rest of the source and publish
COPY SistemaGestaoLar.Api/ SistemaGestaoLar.Api/
WORKDIR /src/SistemaGestaoLar.Api
RUN dotnet publish SistemaGestaoLar.Api.csproj -c Release -o /app/publish --no-restore

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SistemaGestaoLar.Api.dll"]
