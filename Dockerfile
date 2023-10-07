# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./

# Expose the ports your application will listen on
EXPOSE 10939
EXPOSE 10940

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

# Set the ASPNETCORE_URLS environment variable for the runtime stage
ENV ASPNETCORE_URLS=http://+:10939;https://+:10940

ENTRYPOINT ["dotnet", "XmlToJson.dll"]