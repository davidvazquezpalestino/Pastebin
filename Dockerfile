# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY src/Pastebin.Api/Pastebin.Api.csproj src/Pastebin.Api/
COPY src/Pastebin.Core/Pastebin.Core.csproj src/Pastebin.Core/
COPY src/Pastebin.Infrastructure/Pastebin.Infrastructure.csproj src/Pastebin.Infrastructure/
RUN dotnet restore src/Pastebin.Api/Pastebin.Api.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/src/Pastebin.Api
RUN dotnet publish Pastebin.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Pastebin.Api.dll"]
