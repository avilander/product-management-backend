FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj de todos os projetos que ele depende
COPY ProductManagement.Api/ProductManagement.Api.csproj ProductManagement.Api/
COPY ProductManagement.Application/ProductManagement.Application.csproj ProductManagement.Application/
COPY ProductManagement.Domain/ProductManagement.Domain.csproj ProductManagement.Domain/
COPY ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj ProductManagement.Infrastructure/
COPY ProductManagement.Worker/ProductManagement.Worker.csproj ProductManagement.Worker/

RUN dotnet restore ProductManagement.Worker/ProductManagement.Worker.csproj

COPY . .

WORKDIR /src/ProductManagement.Worker
RUN dotnet publish ProductManagement.Worker.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.Worker.dll"]