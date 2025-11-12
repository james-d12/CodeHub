# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY ./aspire ./aspire
COPY --from=backend . ./backend
COPY ./frontend ./frontend

WORKDIR frontend/CodeHub.Portal

RUN dotnet restore CodeHub.Portal.csproj

RUN dotnet publish CodeHub.Portal.csproj -c Release --no-restore -o /app/out/CodeHub.Portal

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build "/app/out/CodeHub.Portal" ./

ENTRYPOINT ["./CodeHub.Portal"]