FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src

COPY src/Shamyr.Cloud.Identity.Client.Test/Shamyr.Cloud.Identity.Client.Test.csproj Shamyr.Cloud.Identity.Client.Test/
COPY src/Shamyr.Cloud.Identity.Client/Shamyr.Cloud.Identity.Client.csproj Shamyr.Cloud.Identity.Client/
COPY src/Shamyr.Cloud.Core/Shamyr.Cloud.Core.csproj Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Gateway.Signal.Client/Shamyr.Cloud.Gateway.Signal.Client.csproj Shamyr.Cloud.Gateway.Signal.Client/
COPY src/Shamyr.Cloud.Gateway.Signal.Messages/Shamyr.Cloud.Gateway.Signal.Messages.csproj Shamyr.Cloud.Gateway.Signal.Messages/
COPY src/Shamyr.Cloud.Swashbuckle/Shamyr.Cloud.Swashbuckle.csproj Shamyr.Cloud.Swashbuckle/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Identity.Client.Test
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Cloud.Identity.Client.Test/ Shamyr.Cloud.Identity.Client.Test/
COPY src/Shamyr.Cloud.Identity.Client/ Shamyr.Cloud.Identity.Client/
COPY src/Shamyr.Cloud.Core/ Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Gateway.Signal.Client/ Shamyr.Cloud.Gateway.Signal.Client/
COPY src/Shamyr.Cloud.Gateway.Signal.Messages/ Shamyr.Cloud.Gateway.Signal.Messages/
COPY src/Shamyr.Cloud.Swashbuckle/ Shamyr.Cloud.Swashbuckle/

WORKDIR Shamyr.Cloud.Identity.Client.Test
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet Shamyr.Cloud.Identity.Service.dll

