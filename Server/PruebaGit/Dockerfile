FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código
COPY . .

# Publicar
RUN dotnet publish -c Release -o /app


# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar base de datos SQLite
COPY database.db /app/database.db

# Copiar appsettings.json (importantísimo)
COPY appsettings.json /app/appsettings.json

# Copiar todo lo compilado
COPY --from=build /app .

ENTRYPOINT ["dotnet", "GitEjercicioSENA.dll"]