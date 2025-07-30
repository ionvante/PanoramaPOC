FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Panorama.API/*.csproj Panorama.API/
RUN dotnet restore Panorama.API/Panorama.API.csproj
COPY Panorama.API/. Panorama.API/
WORKDIR /src/Panorama.API
RUN dotnet publish Panorama.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Panorama.API.dll"]
