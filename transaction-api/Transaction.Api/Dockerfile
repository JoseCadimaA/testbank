# ---------------------------------------
# Build stage
# ---------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto
COPY transaction-api/Transaction.Api/Transaction.Api.csproj ./Transaction.Api/
COPY transaction-api/Transaction.Application/Transaction.Application.csproj ./Transaction.Application/
COPY transaction-api/Transaction.Domain/Transaction.Domain.csproj ./Transaction.Domain/
COPY transaction-api/Transaction.Infrastructure/Transaction.Infrastructure.csproj ./Transaction.Infrastructure/

# Restaurar dependencias
RUN dotnet restore "./Transaction.Api/Transaction.Api.csproj"

# Copiar el resto del código
COPY transaction-api/ ./ 

# Publicar la aplicación
WORKDIR "/src/Transaction.Api"
RUN dotnet publish -c Release -o /app/publish

# ---------------------------------------
# Runtime stage
# ---------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Transaction.Api.dll"]
