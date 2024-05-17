FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CQC.Provider.Connector/CQC.Provider.Connector.csproj", "src/CQC.Provider.Connector/"]
COPY ["src/CQC.Provider.Connector.Core/CQC.Provider.Connector.Core.csproj", "src/CQC.Provider.Connector.Core/"]
COPY ["src/CQC.Provider.Connector.Data/CQC.Provider.Connector.Data.csproj", "src/CQC.Provider.Connector.Data/"]
RUN dotnet restore "src/CQC.Provider.Connector/CQC.Provider.Connector.csproj"
COPY . .
WORKDIR "/src/src/CQC.Provider.Connector"
RUN dotnet build "CQC.Provider.Connector.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CQC.Provider.Connector.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQC.Provider.Connector.dll"]
