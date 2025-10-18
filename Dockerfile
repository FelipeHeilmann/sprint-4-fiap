# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos da solução
COPY *.sln ./
COPY WiseBuddy.Api/*.csproj WiseBuddy.Api/

# Restaura os pacotes
RUN dotnet restore

# Copia o restante do código
COPY . .

# Publica o projeto
WORKDIR /src/WiseBuddy.Api
RUN dotnet publish -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Expõe a porta que a API vai rodar
EXPOSE 5000

# Comando para iniciar a API
ENTRYPOINT ["dotnet", "WiseBuddy.Api.dll"]
