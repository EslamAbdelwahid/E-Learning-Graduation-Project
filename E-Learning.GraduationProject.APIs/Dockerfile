#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["E-Learning.GraduationProject.APIs/E-Learning.GraduationProject.API.csproj", "E-Learning.GraduationProject.APIs/"]
COPY ["E-Learning.GraduationProject.Service/E-Learning.GraduationProject.Service.csproj", "E-Learning.GraduationProject.Service/"]
COPY ["E-Learning.GraduationProject.Repository/E-Learning.GraduationProject.Repository.csproj", "E-Learning.GraduationProject.Repository/"]
COPY ["E-Learning.GraduationProject.Core/E-Learning.GraduationProject.Core.csproj", "E-Learning.GraduationProject.Core/"]
RUN dotnet restore "./E-Learning.GraduationProject.APIs/./E-Learning.GraduationProject.API.csproj"
COPY . .
WORKDIR "/src/E-Learning.GraduationProject.APIs"
RUN dotnet build "./E-Learning.GraduationProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-Learning.GraduationProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Learning.GraduationProject.API.dll"]