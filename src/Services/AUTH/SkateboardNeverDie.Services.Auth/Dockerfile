#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/AUTH/SkateboardNeverDie.Services.Auth/SkateboardNeverDie.Services.Auth.csproj", "src/Services/AUTH/SkateboardNeverDie.Services.Auth/"]
RUN dotnet restore "src/Services/AUTH/SkateboardNeverDie.Services.Auth/SkateboardNeverDie.Services.Auth.csproj"
COPY . .
WORKDIR "/src/src/Services/AUTH/SkateboardNeverDie.Services.Auth"
RUN dotnet build "SkateboardNeverDie.Services.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkateboardNeverDie.Services.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkateboardNeverDie.Services.Auth.dll"]