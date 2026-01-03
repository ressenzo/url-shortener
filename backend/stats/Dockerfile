# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

ARG NUGET_AUTH_TOKEN

# Copy everything into the container
COPY ./src ./src
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY nuget.config .

# Restore dependencies
WORKDIR /app/src/UrlShortener.Stats.Worker
RUN dotnet nuget list source
RUN dotnet nuget update source github \
    --username ressenzo \
    --password $NUGET_AUTH_TOKEN \
    --store-password-in-clear-text \
 	&& dotnet restore

# Build the app
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ConnectionStrings:Mongo=mongodb://admin:admin123@localhost:27017?directConnection=true&serverSelectionTimeoutMS=2000

# Copy build artifacts from build stage
COPY --from=build /app/out ./

# Run the app
ENTRYPOINT ["dotnet", "UrlShortener.Stats.Worker.dll"]
