FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ContactsApi/ContactsApi.csproj", "ContactsApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet nuget locals all --clear

RUN dotnet restore "ContactsApi/ContactsApi.csproj" --force-evaluate

COPY . .

WORKDIR "/src/ContactsApi"

RUN dotnet build --no-restore -c Release -o /app/build /p:EnforceCodeStyleInBuild=false /p:UseRoslynAnalyzers=false

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsApi.dll"]
