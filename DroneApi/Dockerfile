#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DroneApi/DroneApi.csproj", "DroneApi/"]
RUN dotnet restore "./DroneApi/./DroneApi.csproj"
COPY . .
WORKDIR "/src/DroneApi"
RUN dotnet build "./DroneApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Build stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DroneApi.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# Locale stage
FROM debian:9 as locale
RUN apt-get update && apt-get install -y locales && rm -rf /var/lib/apt/lists/* \
    && localedef -i en_US -c -f UTF-8 -A /usr/share/locale/locale.alias en_US.UTF-8

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory to the app directory
WORKDIR /app

# Copy the locale data from the locale stage
COPY --from=locale /usr/lib/locale /usr/lib/locale
COPY --from=locale /usr/share/locale /usr/share/locale

# Set the globalisation environment variables
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG en_US.utf8

# Copy the published web service to the container
COPY --from=publish /app/publish .

# Configure the web service to run on startup
ENTRYPOINT ["dotnet", "DroneApi.dll"]