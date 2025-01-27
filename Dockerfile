# Use the .NET SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use the .NET runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port your app runs on
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "ClothesSalePlatform.dll"]