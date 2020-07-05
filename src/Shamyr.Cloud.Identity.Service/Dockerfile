FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src
COPY src/Shamyr.Cloud.Identity.Service/Shamyr.Cloud.Identity.Service.csproj Shamyr.Cloud.Identity.Service/
COPY NuGet.config .

WORKDIR Shamyr.Cloud.Identity.Service
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src
COPY src/Shamyr.Cloud.Identity.Service/ Shamyr.Cloud.Identity.Service/

WORKDIR Shamyr.Cloud.Identity.Service
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shamyr.Cloud.Identity.Service.dll"]
