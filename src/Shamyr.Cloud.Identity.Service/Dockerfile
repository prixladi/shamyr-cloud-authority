FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src
COPY src/Shamyr.Cloud.Identity.Service/Shamyr.Cloud.Identity.Service.csproj Shamyr.Cloud.Identity.Service/
COPY src/Shamyr.Cloud.Identity.Models/Shamyr.Cloud.Identity.Models.csproj Shamyr.Cloud.Identity.Models/
COPY src/Shamyr.Cloud.Database/Shamyr.Cloud.Database.csproj Shamyr.Cloud.Database/
COPY src/Shamyr.Cloud.Bson/Shamyr.Cloud.Bson.csproj Shamyr.Cloud.Bson/ 
COPY src/Shamyr.Cloud.Core/Shamyr.Cloud.Core.csproj Shamyr.Cloud.Core/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Identity.Service
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Cloud.Identity.Service/ Shamyr.Cloud.Identity.Service/ 
COPY src/Shamyr.Cloud.Identity.Models/ Shamyr.Cloud.Identity.Models/
COPY src/Shamyr.Cloud.Database/ Shamyr.Cloud.Database/ 
COPY src/Shamyr.Cloud.Bson/ Shamyr.Cloud.Bson/ 
COPY src/Shamyr.Cloud.Core/ Shamyr.Cloud.Core/ 

WORKDIR Shamyr.Cloud.Identity.Service
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet Shamyr.Cloud.Identity.Service.dll 
