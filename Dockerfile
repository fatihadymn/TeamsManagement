#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TeamsManagement/TeamsManagement.Api.csproj", "TeamsManagement/"]
RUN dotnet restore "TeamsManagement/TeamsManagement.Api.csproj"
COPY . .
WORKDIR "/src/TeamsManagement"
RUN dotnet build "TeamsManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamsManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamsManagement.Api.dll"]