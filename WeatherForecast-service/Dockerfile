# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore WeatherForecast.csproj
RUN dotnet publish WeatherForecast.csproj -c Release -o /app/publish

# Use the official ASP.NET runtime image for run
FROM mcr.microsoft.com/d