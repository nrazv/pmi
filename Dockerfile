# -----------------------------------------
# Step 1: Parrot OS base + ASP.NET Runtime
# -----------------------------------------
FROM docker.io/parrotsec/security:6.3 AS base

# Install system deps + Microsoft repo for .NET
RUN apt update && apt install -y \
    wget \
    ca-certificates \
    apt-transport-https \
    software-properties-common \
    libc6 \
    libssl3 \
    libicu72 \
    zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Add Microsoft package repository
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb

# Install ASP.NET Runtime 9
RUN apt update && apt install -y aspnetcore-runtime-9.0 \
    && rm -rf /var/lib/apt/lists/*


WORKDIR /app
EXPOSE 8080



# -----------------------------------------
# Step 2: Build stage (.NET SDK on Parrot)
# -----------------------------------------
FROM docker.io/parrotsec/security:6.3 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Install build dependencies
RUN apt update && apt install -y \
    wget \
    ca-certificates \
    apt-transport-https \
    software-properties-common \
    libc6 \
    libssl3 \
    libicu72 \
    zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Add Microsoft repo
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb

# Install .NET SDK 9
RUN apt update && apt install -y dotnet-sdk-9.0 \
    && rm -rf /var/lib/apt/lists/*

# Copy project file and restore
COPY install-tools.sh ./pmi/
COPY pmi/pmi.csproj ./pmi/
WORKDIR /src/pmi
RUN dotnet restore

# Copy the rest of backend source
COPY pmi/. .

RUN dotnet build ./pmi.csproj -c $BUILD_CONFIGURATION -o /app/build /p:UseAppHost=false


# -----------------------------------------
# Step 3: Build React frontend
# -----------------------------------------
FROM node:20 AS pmi-interface-client
WORKDIR /src/pmi-interface

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
# Step 5: Final runtime image (Parrot)
# -----------------------------------------
FROM base AS final

WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
# Copy published backend
COPY --from=publish /app/publish .

# Copy React build to wwwroot
COPY --from=pmi-interface-client /src/pmi-interface/build ./wwwroot

ENTRYPOINT ["dotnet", "pmi.dll"]
