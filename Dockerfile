# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build and publish app to the /out directory
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from previous stage
COPY --from=build /out ./

# Expose the default port (change if needed)
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "dotnet_sp_api.dll"]

