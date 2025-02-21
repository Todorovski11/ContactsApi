# ✅ Base Image (For Running the App)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ✅ Build Image (For Building the App)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔥 COPY ALL .csproj FILES (Avoid missing dependencies)
COPY ["ContactsApi/ContactsApi.csproj", "ContactsApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# 🔥 Force Clear NuGet Cache
RUN dotnet nuget locals all --clear

# 🔥 Restore Dependencies (Force Full Resolution)
RUN dotnet restore "ContactsApi/ContactsApi.csproj" --force-evaluate

# ✅ COPY ALL REMAINING SOURCE FILES
COPY . .

WORKDIR "/src/ContactsApi"

# ✅ Build the App
RUN dotnet build --no-restore -c Release -o /app/build /p:EnforceCodeStyleInBuild=false /p:UseRoslynAnalyzers=false

# ✅ Publish the App
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# ✅ Final Runtime Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsApi.dll"]
