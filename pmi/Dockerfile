# Step 1: Base stage for runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base

COPY install-tools.sh /tmp/install-tools.sh
RUN chmod +x /tmp/install-tools.sh
RUN /tmp/install-tools.sh

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Step 2: Build stage to compile the application
FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["pmi.csproj", "."]
RUN dotnet restore "./pmi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./pmi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Step 3: Publish stage to publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./pmi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Step 4: Final stage to run the application (runtime environment)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pmi.dll"]