# Use official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./OrderControllerMicroservice.csproj"
RUN dotnet publish "./OrderControllerMicroservice.csproj" -c Release -o /app/publish

# Use official .NET runtime image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "OrderControllerMicroservice.dll"]
