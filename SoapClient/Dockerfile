FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SoapClient/SoapClient.csproj", "SoapClient/"]
RUN dotnet restore "SoapClient/SoapClient.csproj"
COPY . .
WORKDIR "/src/SoapClient"
RUN dotnet build "SoapClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoapClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoapClient.dll"]
