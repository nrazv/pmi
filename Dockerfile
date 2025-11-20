# -----------------------------------------
# Step 1: Base stage for runtime environment
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base

# Install tools
COPY install-tools.sh /tmp/install-tools.sh
RUN chmod +x /tmp/install-tools.sh && /tmp/install-tools.sh

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# -----------------------------------------
# Step 2: Build stage for .NET backend
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy project file and restore
COPY install-tools.sh ./pmi/
COPY pmi/pmi.csproj ./pmi/
WORKDIR /src/pmi
RUN dotnet restore

# Copy rest of the backend source code
COPY pmi/. .

# Build (with no apphost to avoid folder vs file conflict)
RUN dotnet build ./pmi.csproj -c $BUILD_CONFIGURATION -o /app/build /p:UseAppHost=false

# -----------------------------------------
# Step 3: Build React frontend
# -----------------------------------------
FROM node:20 AS pmi-interface-client
WORKDIR /src/pmi-interface

# Install deps and build React app
COPY pmi-interface/package*.json ./
RUN npm install

COPY pmi-interface/ .
RUN npm run build

# -----------------------------------------
# Step 4: Publish .NET backend
# -----------------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

WORKDIR /src/pmi
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -----------------------------------------
# Step 5: Final runtime image
# -----------------------------------------
FROM base AS final

WORKDIR /app

# Copy published .NET app
COPY --from=publish /app/publish .

# Copy React frontend build to wwwroot
COPY --from=pmi-interface-client /src/pmi-interface/build ./wwwroot

ENTRYPOINT ["dotnet", "pmi.dll"]