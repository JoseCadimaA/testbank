# ---------------------------------------
# Build stage
# ---------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto
COPY Anti-Fraud/AntiFraud.Api/AntiFraud.Api.csproj ./AntiFraud.Api/
COPY Anti-Fraud/AntiFraud.Application/AntiFraud.Application.csproj ./AntiFraud.Application/
COPY Anti-Fraud/AntiFraud.Domain/AntiFraud.Domain.csproj ./AntiFraud.Domain/
COPY Anti-Fraud/AntiFraud.Infrastructure/AntiFraud.Infrastructure.csproj ./AntiFraud.Infrastructure/

# Restaurar dependencias
RUN dotnet restore "./AntiFraud.Api/AntiFraud.Api.csproj"

# Copiar el resto del código
COPY Anti-Fraud/ ./ 

# Publicar la aplicación
WORKDIR "/src/AntiFraud.Api"
RUN dotnet publish -c Release -o /app/publish

# ---------------------------------------
# Runtime stage
# ---------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "AntiFraud.Api.dll"]
