# Use the .NET SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY SalePlatform/SalePlatform/*.csproj ./SalePlatform/
WORKDIR /app/SalePlatform
RUN dotnet restore

# Copy the rest of the files and build the app
COPY SalePlatform/SalePlatform/. ./
RUN dotnet publish -c Release -o /out

# Use the .NET runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out .

# Expose the port your app runs on
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "ClothesSalePlatform.dll"]