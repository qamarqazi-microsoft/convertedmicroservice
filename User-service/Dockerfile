
# Dockerfile for User-service
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["User-service.csproj", "./"]
RUN dotnet restore "User-service.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "User-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User-service.dll"]
