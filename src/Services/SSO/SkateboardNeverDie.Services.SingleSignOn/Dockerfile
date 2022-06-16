#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/SSO/SkateboardNeverDie.Services.SingleSignOn/SkateboardNeverDie.Services.SingleSignOn.csproj", "src/Services/SSO/SkateboardNeverDie.Services.SingleSignOn/"]
RUN dotnet restore "src/Services/SSO/SkateboardNeverDie.Services.SingleSignOn/SkateboardNeverDie.Services.SingleSignOn.csproj"
COPY . .
WORKDIR "/src/src/Services/SSO/SkateboardNeverDie.Services.SingleSignOn"
RUN dotnet build "SkateboardNeverDie.Services.SingleSignOn.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkateboardNeverDie.Services.SingleSignOn.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkateboardNeverDie.Services.SingleSignOn.dll"]