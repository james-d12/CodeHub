# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY ./src/backend .
COPY ./Directory.Build.props .

WORKDIR CodeHub.Api

RUN dotnet restore CodeHub.Api.csproj

RUN dotnet publish CodeHub.Api.csproj -c Release --no-restore -o /app/out/CodeHub.Api

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build "/app/out/CodeHub.Api" ./

ENTRYPOINT ["./CodeHub.Api"]