# Use .NET SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ./SalePlatform/SalePlatform/*.csproj ./SalePlatform/
WORKDIR /src/SalePlatform
RUN dotnet restore

# Copy all project files and build the project
COPY ./SalePlatform/SalePlatform/. ./SalePlatform/
RUN dotnet build -c Release -o /app/build

# Publish stage
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Use .NET runtime for final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ClothesSalePlatform.dll"]
