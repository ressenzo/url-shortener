# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything into the container
COPY ./src ./src

# Restore dependencies
WORKDIR /app/src/UrlShortener.Api
RUN dotnet restore

# Build the app
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ConnectionStrings:Mongo=mongodb://admin:admin123@localhost:27017?directConnection=true&serverSelectionTimeoutMS=2000

# Copy build artifacts from build stage
COPY --from=build /app/out ./

# Expose port (change to your API port if different)
EXPOSE 5147

# Run the app
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]
