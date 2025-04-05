#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER root
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS="http://+:80"
ENV ASPNETCORE_ENVIRONMENT="Development"

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend.Erp.Skeleton.Api/Backend.Erp.Skeleton.Api.csproj", "Backend.Erp.Skeleton.Api/"]
COPY ["Backend.Erp.Skeleton.Application/Backend.Erp.Skeleton.Application.csproj", "Backend.Erp.Skeleton.Application/"]
COPY ["Backend.Erp.Skeleton.Domain/Backend.Erp.Skeleton.Domain.csproj", "Backend.Erp.Skeleton.Domain/"]
COPY ["Backend.Erp.Skeleton.Infrastructure/Backend.Erp.Skeleton.Infrastructure.csproj", "Backend.Erp.Skeleton.Infrastructure/"]
RUN dotnet restore "./Backend.Erp.Skeleton.Api/Backend.Erp.Skeleton.Api.csproj"
COPY . .
WORKDIR "/src/Backend.Erp.Skeleton.Api"
RUN dotnet build "./Backend.Erp.Skeleton.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend.Erp.Skeleton.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Erp.Skeleton.Api.dll"]