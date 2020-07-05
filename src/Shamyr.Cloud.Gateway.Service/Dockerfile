FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src
COPY src/Shamyr.Cloud.Gateway.Service/Shamyr.Cloud.Gateway.Service.csproj Shamyr.Cloud.Gateway.Service/
COPY src/Shamyr.Cloud.Gateway.Signal.Messages/Shamyr.Cloud.Gateway.Signal.Messages.csproj Shamyr.Cloud.Gateway.Signal.Messages/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Gateway.Service
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src
COPY src/Shamyr.Cloud.Gateway.Service/ Shamyr.Cloud.Gateway.Service/
COPY src/Shamyr.Cloud.Gateway.Signal.Messages/ Shamyr.Cloud.Gateway.Signal.Messages/

WORKDIR Shamyr.Cloud.Gateway.Service
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shamyr.Cloud.Gateway.Service.dll"]
