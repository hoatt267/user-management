# === STAGE 1: BUILD ===
# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the solution file and project files first! 
# Why? Docker caches layers. If you don't change your .csproj, Docker skips this step (Fast builds!)
COPY UserManagementApp.slnx ./
COPY src/UserManagementApp.API/UserManagementApp.API.csproj src/UserManagementApp.API/
COPY src/UserManagementApp.Application/UserManagementApp.Application.csproj src/UserManagementApp.Application/
COPY src/UserManagementApp.Domain/UserManagementApp.Domain.csproj src/UserManagementApp.Domain/
COPY src/UserManagementApp.Infrastructure/UserManagementApp.Infrastructure.csproj src/UserManagementApp.Infrastructure/

# Restore dependencies (NuGet packages)
RUN dotnet restore src/UserManagementApp.API/UserManagementApp.API.csproj

# Copy the rest of the source code
COPY . .

# Build and Publish the application to a folder named 'out'
WORKDIR /app/src/UserManagementApp.API
RUN dotnet publish -c Release -o /app/out

# === STAGE 2: RUNTIME ===
# Use the lightweight ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy the built files from Stage 1
COPY --from=build /app/out .

# Expose port 8080 (Default for .NET 8 containers)
EXPOSE 8080

# The entry point: What command runs when the container starts?
ENTRYPOINT ["dotnet", "UserManagementApp.API.dll"]