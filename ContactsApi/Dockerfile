# ✅ Base Image (For Running the App)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ✅ Build Image (For Building the App)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔥 COPY ONLY .csproj FIRST (Optimizes Docker Build Cache)
COPY ["ContactsApi/ContactsApi.csproj", "ContactsApi/"]

# 🔥 CLEAR NUGET CACHE (To fix missing dependencies)
RUN dotnet nuget locals all --clear

# 🔥 RESTORE DEPENDENCIES
WORKDIR "/src/ContactsApi"
RUN dotnet restore "ContactsApi.csproj" --force-evaluate

# ✅ COPY REMAINING FILES AFTER RESTORE
COPY ContactsApi/ ContactsApi/

# ✅ Build the App
WORKDIR "/src/ContactsApi"
RUN dotnet build --no-restore -c Release -o /app/build

# ✅ Publish the App
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# ✅ Final Runtime Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsApi.dll"]
