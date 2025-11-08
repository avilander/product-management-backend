FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copia os csproj de TODOS os projetos (API + camadas)
COPY ProductManagement.Api/ProductManagement.Api.csproj ProductManagement.Api/
COPY ProductManagement.Application/ProductManagement.Application.csproj ProductManagement.Application/
COPY ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj ProductManagement.Infrastructure/
COPY ProductManagement.Domain/ProductManagement.Domain.csproj ProductManagement.Domain/

# 2) Restaura dependências com base na API (que referencia os outros)
RUN dotnet restore ProductManagement.Api/ProductManagement.Api.csproj

# 3) Copia o restante do código da solução
COPY . .

# 4) Publica a API
WORKDIR /src/ProductManagement.Api
RUN dotnet publish ProductManagement.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ProductManagement.Api.dll"]
