# syntax=docker/dockerfile:1

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files first for better layer caching
COPY SistemaGestaoLar.Front/SistemaGestaoLar.Front.csproj SistemaGestaoLar.Front/
COPY SistemaGestaoLar.Api.Client/SistemaGestaoLar.Api.Client.csproj SistemaGestaoLar.Api.Client/
RUN dotnet restore SistemaGestaoLar.Front/SistemaGestaoLar.Front.csproj

# Copy the rest of the source and publish
COPY SistemaGestaoLar.Front/ SistemaGestaoLar.Front/
COPY SistemaGestaoLar.Api.Client/ SistemaGestaoLar.Api.Client/
WORKDIR /src/SistemaGestaoLar.Front
RUN dotnet publish SistemaGestaoLar.Front.csproj -c Release -o /app/publish --no-restore

# ---- Runtime stage (static files served by nginx) ----
FROM nginx:alpine AS final
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY SistemaGestaoLar.Front/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
