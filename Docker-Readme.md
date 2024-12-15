
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

# Publicando a aplica��o para produ��o
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Imagem final com a aplica��o pronta para execu��o
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
```

### Passo 2: Configura��o do `docker-compose.yml`

Caso tenhamos m�ltiplos projetos ou depend�ncias, voc� pode usar o `docker-compose`. Caso contr�rio, este passo pode ser opcional, mas � �til quando se deseja gerenciar m�ltiplos containers.

#### docker-compose.yml (se necess�rio)

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

1. **Adicionar Suporte Docker (qdo necess�rio)**:
   - No **Solution Explorer**, clique com o bot�o direito no projeto **Api**.
   - Selecione **Add** > **Docker Support** (Essa op��o deve estar dispon�vel no Visual Studio 2022).
   - Escolha o **.NET 8.0** como a vers�o de execu��o (se dispon�vel).

   Qdo a op��o **Add Docker Support** n�o aparecer, isso significa que j� foi adicionado o Docker manualmente ou que a configura��o est� sendo gerida por outro arquivo (`Dockerfile`).

2. **Definir o Projeto de In�cio**:
   - Clique com o bot�o direito no projeto `Api` e selecione **Set as Startup Project**.

3. **Configura��o do Docker**:
   - Certificar de que o Visual Studio est� configurado para usar o Docker como o ambiente de execu��o.
   - No topo da interface do Visual Studio, onde normalmente se v� a op��o de **IIS Express** ou **Project**, agora voc� ver� uma op��o chamada **Docker**.
   - Selecione **Docker** como o perfil de execu��o.

### Passo 4: Executar o Projeto no Docker

Para rodar a aplica��o usando o Docker, basta clicar em **Start** (ou pressionar `Ctrl + F5`) no Visual Studio. O Docker ir� construir e rodar a imagem com base no `Dockerfile` fornecido.

### Passo 5: Verificar a Aplica��o

- Quando o Visual Studio terminar de construir e rodar a aplica��o, voc� pode acessar sua API em `http://localhost:80`.

### Passo 6: Rodar Manualmente com Docker (Alternativa)

Se preferir rodar o container manualmente, voc� pode seguir os seguintes passos:

1. Abra o terminal na pasta onde o `Dockerfile` est� localizado.
2. **Construir a imagem Docker**:

   ```bash
   docker build -t taskmanagerapi .
   ```

3. **Rodar o container Docker**:

   ```bash
   docker run -d -p 80:80 taskmanagerapi
   ```

4. **Verificar se o container est� funcionando**:

   ```bash
   docker ps
  