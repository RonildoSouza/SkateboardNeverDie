#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/API/SkateboardNeverDie.Services.Api/SkateboardNeverDie.Services.Api.csproj", "src/Services/API/SkateboardNeverDie.Services.Api/"]
COPY ["src/Services/API/SkateboardNeverDie.Infrastructure.Configuration/SkateboardNeverDie.Infrastructure.Configuration.csproj", "src/Services/API/SkateboardNeverDie.Infrastructure.Configuration/"]
COPY ["src/Services/API/SkateboardNeverDie.Infrastructure/SkateboardNeverDie.Infrastructure.csproj", "src/Services/API/SkateboardNeverDie.Infrastructure/"]
COPY ["src/Services/API/SkateboardNeverDie.Core.Infrastructure/SkateboardNeverDie.Core.Infrastructure.csproj", "src/Services/API/SkateboardNeverDie.Core.Infrastructure/"]
COPY ["src/Services/API/SkateboardNeverDie.Core.Application/SkateboardNeverDie.Core.Application.csproj", "src/Services/API/SkateboardNeverDie.Core.Application/"]
COPY ["src/Services/API/SkateboardNeverDie.Core.Domain/SkateboardNeverDie.Core.Domain.csproj", "src/Services/API/SkateboardNeverDie.Core.Domain/"]
COPY ["src/Services/API/SkateboardNeverDie.Domain/SkateboardNeverDie.Domain.csproj", "src/Services/API/SkateboardNeverDie.Domain/"]
COPY ["src/Services/API/SkateboardNeverDie.Application/SkateboardNeverDie.Application.csproj", "src/Services/API/SkateboardNeverDie.Application/"]
RUN dotnet restore "src/Services/API/SkateboardNeverDie.Services.Api/SkateboardNeverDie.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/API/SkateboardNeverDie.Services.Api"
RUN dotnet build "SkateboardNeverDie.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkateboardNeverDie.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkateboardNeverDie.Services.Api.dll"]