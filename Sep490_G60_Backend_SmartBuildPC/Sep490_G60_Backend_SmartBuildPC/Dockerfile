#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sep490_G60_Backend_SmartBuildPC/Sep490_G60_Backend_SmartBuildPC.csproj", "Sep490_G60_Backend_SmartBuildPC/"]
RUN dotnet restore "Sep490_G60_Backend_SmartBuildPC/Sep490_G60_Backend_SmartBuildPC.csproj"
COPY . .
WORKDIR "/src/Sep490_G60_Backend_SmartBuildPC"
RUN dotnet build "Sep490_G60_Backend_SmartBuildPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sep490_G60_Backend_SmartBuildPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sep490_G60_Backend_SmartBuildPC.dll"]