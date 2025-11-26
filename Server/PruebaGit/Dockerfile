FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Agregar esta línea:
COPY database.db /app/database.db

COPY --from=build /app .
ENTRYPOINT ["dotnet", "GitEjercicioSENA.dll"]