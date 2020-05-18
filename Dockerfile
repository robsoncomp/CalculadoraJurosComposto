FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS dotnet-builder
    
WORKDIR /app
COPY . /app

RUN dotnet restore CalculaJuros.API/CalculaJuros.API.csproj -nowarn:msb3202,nu1503
RUN dotnet build CalculaJuros.API/CalculaJuros.API.csproj -c Release -o /app/publish
RUN dotnet publish CalculaJuros.API/CalculaJuros.API.csproj -c Release -o /app/publish

### Estágio 2 - Subir a aplicação através dos binários ###
FROM microsoft/dotnet:2.2-aspnetcore-runtime
ENV ASPNETCORE_ENVIRONMENT=Staging \
    LC_ALL=pt_BR.UTF-8 \
    LANG=pt_BR.UTF-8 \
    ASPNETCORE_URLS=http://*:80 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
EXPOSE 80
COPY --from=dotnet-builder /app/publish .
ENTRYPOINT ["dotnet", "CalculaJuros.API.dll"]