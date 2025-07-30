FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# copy csproj and restore as distinct layers
COPY Panorama.API/Panorama.API.csproj Panorama.API/
RUN dotnet restore Panorama.API/Panorama.API.csproj

# copy everything else and publish
COPY Panorama.API/ Panorama.API/
WORKDIR /src/Panorama.API
RUN dotnet publish Panorama.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# configure runtime
ENV ASPNETCORE_URLS=http://+:80 \
    ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
EXPOSE 443
