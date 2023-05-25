FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ARG SERVER_BUILDDIR
WORKDIR ${SERVER_BUILDDIR}

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY . .
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

ARG SERVER_WORKDIR
WORKDIR ${SERVER_WORKDIR}

COPY --from=build /app .

ENTRYPOINT ["dotnet", "server.dll"]