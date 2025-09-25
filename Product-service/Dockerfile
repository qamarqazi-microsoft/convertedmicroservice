
# Dockerfile for Product-service
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Product-service.csproj", "./"]
RUN dotnet restore "Product-service.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Product-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product-service.dll"]
