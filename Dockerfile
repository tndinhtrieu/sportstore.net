FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 44304
EXPOSE 44304

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY SportsStore/SportsStore.csproj SportsStore/
RUN dotnet restore SportsStore/SportsStore.csproj
COPY . .
WORKDIR /src/SportsStore
RUN dotnet build SportsStore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SportsStore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SportsStore.dll"]
