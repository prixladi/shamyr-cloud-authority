#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src

COPY src/Shamyr.Cloud.Authority.Service/Shamyr.Cloud.Authority.Service.csproj Shamyr.Cloud.Authority.Service/
COPY src/Shamyr.Cloud.Authority.Signal.Messages/Shamyr.Cloud.Authority.Signal.Messages.csproj Shamyr.Cloud.Authority.Signal.Messages/
COPY src/Shamyr.Cloud.Authority.Token.Models/Shamyr.Authority.Token.Models.csproj Shamyr.Cloud.Authority.Token.Models/
COPY src/Shamyr.Cloud.Database/Shamyr.Cloud.Database.csproj Shamyr.Cloud.Database/
COPY src/Shamyr.Cloud.Bson/Shamyr.Cloud.Bson.csproj Shamyr.Cloud.Bson/
COPY src/Shamyr.Cloud.Core/Shamyr.Cloud.Core.csproj Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Swashbuckle.Bson/Shamyr.Cloud.Swashbuckle.Bson.csproj Shamyr.Cloud.Swashbuckle.Bson/
COPY src/Shamyr.Cloud.Swashbuckle/Shamyr.Cloud.Swashbuckle.csproj Shamyr.Cloud.Swashbuckle/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Authority.Service
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Cloud.Authority.Service/ Shamyr.Cloud.Authority.Service/
COPY src/Shamyr.Cloud.Authority.Signal.Messages/ Shamyr.Cloud.Authority.Signal.Messages/
COPY src/Shamyr.Cloud.Authority.Token.Models/ Shamyr.Cloud.Authority.Token.Models/
COPY src/Shamyr.Cloud.Database/ Shamyr.Cloud.Database/
COPY src/Shamyr.Cloud.Bson/ Shamyr.Cloud.Bson/
COPY src/Shamyr.Cloud.Core/ Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Swashbuckle.Bson/ Shamyr.Cloud.Swashbuckle.Bson/
COPY src/Shamyr.Cloud.Swashbuckle/ Shamyr.Cloud.Swashbuckle/

WORKDIR Shamyr.Cloud.Authority.Service
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet Shamyr.Cloud.Authority.Service.dll"]
