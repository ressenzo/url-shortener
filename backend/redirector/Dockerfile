# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy everything into the container
COPY ./src ./src
COPY Directory.Packages.props .
COPY Directory.Build.props .

# Restore dependencies
WORKDIR /app/src/UrlShortener.Redirector.Api
RUN dotnet restore

# Build the app
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy build artifacts from build stage
COPY --from=build /app/out ./

# Expose port (change to your API port if different)
EXPOSE 5147

# Run the app
ENTRYPOINT ["dotnet", "UrlShortener.Redirector.Api.dll"]
