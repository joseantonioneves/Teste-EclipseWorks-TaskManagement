
### Passo 1: Atualizar o Dockerfile para .NET 8.0

`Dockerfile` para usar o SDK e a runtime do **.NET 8.0**.

#### Dockerfile atualizado para .NET 8.0

```dockerfile
# Imagem base para ASP.NET Core 8.0 (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagem base para o SDK .NET 8.0 para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

# Publicando a aplicação para produção
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Imagem final com a aplicação pronta para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
```

### Passo 2: Configuração do `docker-compose.yml`

Caso tenhamos múltiplos projetos ou dependências, você pode usar o `docker-compose`. Caso contrário, este passo pode ser opcional, mas é útil quando se deseja gerenciar múltiplos containers.

#### docker-compose.yml (se necessário)

```yaml
version: '3.8'

services:
  taskmanagerapi:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "80:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
```

### Passo 3: Adicionar Suporte ao Docker no Visual Studio

1. **Adicionar Suporte Docker (qdo necessário)**:
   - No **Solution Explorer**, clique com o botão direito no projeto **Api**.
   - Selecione **Add** > **Docker Support** (Essa opção deve estar disponível no Visual Studio 2022).
   - Escolha o **.NET 8.0** como a versão de execução (se disponível).

   Qdo a opção **Add Docker Support** não aparecer, isso significa que já foi adicionado o Docker manualmente ou que a configuração está sendo gerida por outro arquivo (`Dockerfile`).

2. **Definir o Projeto de Início**:
   - Clique com o botão direito no projeto `Api` e selecione **Set as Startup Project**.

3. **Configuração do Docker**:
   - Certificar de que o Visual Studio está configurado para usar o Docker como o ambiente de execução.
   - No topo da interface do Visual Studio, onde normalmente se vê a opção de **IIS Express** ou **Project**, agora você verá uma opção chamada **Docker**.
   - Selecione **Docker** como o perfil de execução.

### Passo 4: Executar o Projeto no Docker

Para rodar a aplicação usando o Docker, basta clicar em **Start** (ou pressionar `Ctrl + F5`) no Visual Studio. O Docker irá construir e rodar a imagem com base no `Dockerfile` fornecido.

### Passo 5: Verificar a Aplicação

- Quando o Visual Studio terminar de construir e rodar a aplicação, você pode acessar sua API em `http://localhost:80`.

### Passo 6: Rodar Manualmente com Docker (Alternativa)

Se preferir rodar o container manualmente, você pode seguir os seguintes passos:

1. Abra o terminal na pasta onde o `Dockerfile` está localizado.
2. **Construir a imagem Docker**:

   ```bash
   docker build -t taskmanagerapi .
   ```

3. **Rodar o container Docker**:

   ```bash
   docker run -d -p 80:80 taskmanagerapi
   ```

4. **Verificar se o container está funcionando**:

   ```bash
   docker ps
  