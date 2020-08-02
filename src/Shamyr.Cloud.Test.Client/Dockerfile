FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src

COPY src/Shamyr.Cloud.Test.Client/Shamyr.Cloud.Test.Client.csproj Shamyr.Cloud.Test.Client/
COPY src/Shamyr.Cloud.Identity.Client/Shamyr.Cloud.Identity.Client.csproj Shamyr.Cloud.Identity.Client/
COPY src/Shamyr.Cloud.Identity.Models/Shamyr.Cloud.Identity.Models.csproj Shamyr.Cloud.Identity.Models/
COPY src/Shamyr.Cloud.Core/Shamyr.Cloud.Core.csproj Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Authority.Signal.Client/Shamyr.Cloud.Authority.Signal.Client.csproj Shamyr.Cloud.Authority.Signal.Client/
COPY src/Shamyr.Cloud.Authority.Signal.Messages/Shamyr.Cloud.Authority.Signal.Messages.csproj Shamyr.Cloud.Authority.Signal.Messages/
COPY src/Shamyr.Cloud.Authority.Token.Models/Shamyr.Authority.Token.Models.csproj Shamyr.Cloud.Authority.Token.Models/
COPY src/Shamyr.Cloud.Swashbuckle/Shamyr.Cloud.Swashbuckle.csproj Shamyr.Cloud.Swashbuckle/

COPY NuGet.config .

WORKDIR Shamyr.Cloud.Identity.Client.Test
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Cloud.Test.Client/ Shamyr.Cloud.Test.Client/
COPY src/Shamyr.Cloud.Identity.Client/ Shamyr.Cloud.Identity.Client/
COPY src/Shamyr.Cloud.Identity.Models/ Shamyr.Cloud.Identity.Models/
COPY src/Shamyr.Cloud.Core/ Shamyr.Cloud.Core/
COPY src/Shamyr.Cloud.Authority.Signal.Client/ Shamyr.Cloud.Authority.Signal.Client/
COPY src/Shamyr.Cloud.Authority.Signal.Messages/ Shamyr.Cloud.Authority.Signal.Messages/
COPY src/Shamyr.Cloud.Authority.Token.Models/ Shamyr.Cloud.Authority.Token.Models/
COPY src/Shamyr.Cloud.Swashbuckle/ Shamyr.Cloud.Swashbuckle/

WORKDIR Shamyr.Cloud.Identity.Client.Test
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet Shamyr.Cloud.Identity.Service.dll

