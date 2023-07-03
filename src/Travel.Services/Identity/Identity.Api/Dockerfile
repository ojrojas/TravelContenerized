#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Travel.Services/Identity/Identity.Api/Identity.Api.csproj", "src/Travel.Services/Identity/Identity.Api/"]
COPY ["src/Travel.Services/Identity/Identity.Core/Identity.Core.csproj", "src/Travel.Services/Identity/Identity.Core/"]
COPY ["src/Travel.BuildingBlocks/Travel.Repository/Travel.Repository.csproj", "src/Travel.BuildingBlocks/Travel.Repository/"]
COPY ["src/Travel.BuildingBlocks/Travel.BaseHttps/Travel.BaseHttps.csproj", "src/Travel.BuildingBlocks/Travel.BaseHttps/"]
RUN dotnet restore "src/Travel.Services/Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Travel.Services/Identity/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
