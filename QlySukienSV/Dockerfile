# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QlySukienSV/QlySukienSV.csproj", "QlySukienSV/"]
RUN dotnet restore "./QlySukienSV/QlySukienSV.csproj"
COPY . .
WORKDIR "/src/QlySukienSV"
RUN dotnet build "./QlySukienSV.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publish ứng dụng ra thư mục /app/publish
RUN dotnet publish "./QlySukienSV.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final

# Đặt working directory trong container
WORKDIR /app

# Copy published app từ publish stage vào container
# /app/publish (từ publish stage) → /app (trong final container)
COPY --from=publish /app/publish .

# Định nghĩa lệnh chạy khi container start
# dotnet QlySukienSV.dll sẽ:
# 1. Tự động chạy migrations (code trong Program.cs)
# 2. Start web application
ENTRYPOINT ["dotnet", "QlySukienSV.dll"]