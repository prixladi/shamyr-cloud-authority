FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
# restore
WORKDIR /src

COPY src/Shamyr.Cloud.Authority.Client.Test/Shamyr.Cloud.Test.Client.csproj Shamyr.Cloud.Authority.Client.Test/
COPY src/Shamyr.Cloud.Core/Shamyr.Cloud.Core.csproj Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Authority.Client/Shamyr.Cloud.Authority.Client.csproj Shamyr.Cloud.Authority.Client/
COPY src/Shamyr.Cloud.Authority.Models/Shamyr.Cloud.Authority.Models.csproj Shamyr.Cloud.Authority.Models/
COPY src/Shamyr.Cloud.Swashbuckle/Shamyr.Cloud.Swashbuckle.csproj Shamyr.Cloud.Swashbuckle/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Test.Client
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Cloud.Authority.Client.Test/ Shamyr.Cloud.Authority.Client.Test/
COPY src/Shamyr.Cloud.Core/ Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Authority.Client/ Shamyr.Cloud.Authority.Client/
COPY src/Shamyr.Cloud.Authority.Models/ Shamyr.Cloud.Authority.Models/
COPY src/Shamyr.Cloud.Swashbuckle/ Shamyr.Cloud.Swashbuckle/

WORKDIR Shamyr.Cloud.Test.Client
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet Shamyr.Cloud.Identity.Service.dll

