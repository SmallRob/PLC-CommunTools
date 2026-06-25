# docker/server.Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY Directory.Build.props .
COPY ["src/Server/Commun.Server.API/Commun.Server.API.csproj", "Server/Commun.Server.API/"]
COPY ["src/Core/Commun.Protocols/Commun.Protocols.csproj", "Core/Commun.Protocols/"]
COPY ["src/Core/Commun.Modbus/Commun.Modbus.csproj", "Core/Commun.Modbus/"]
COPY ["src/Core/Commun.MQTT/Commun.MQTT.csproj", "Core/Commun.MQTT/"]
COPY ["src/Infrastructure/Commun.Data/Commun.Data.csproj", "Infrastructure/Commun.Data/"]
COPY ["src/Infrastructure/Commun.Logging/Commun.Logging.csproj", "Infrastructure/Commun.Logging/"]

# Restore dependencies
RUN dotnet restore "Server/Commun.Server.API/Commun.Server.API.csproj"

# Copy source code
COPY src/Server/Commun.Server.API/ Server/Commun.Server.API/
COPY src/Core/Commun.Protocols/ Core/Commun.Protocols/
COPY src/Core/Commun.Modbus/ Core/Commun.Modbus/
COPY src/Core/Commun.MQTT/ Core/Commun.MQTT/
COPY src/Infrastructure/Commun.Data/ Infrastructure/Commun.Data/
COPY src/Infrastructure/Commun.Logging/ Infrastructure/Commun.Logging/

# Build
WORKDIR "/src/Server/Commun.Server.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commun.Server.API.dll"]
