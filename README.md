# TaskManager
---
Este projeto é um protótipo para funcionalidades de gerenciar tarefas dentro de um sistema de gerenciamento de projetos. Ele permite criar, editar e gerenciar tarefas de forma simples e eficiente. O foco do sistema é fornecer uma interface robusta para a criação e atualização de tarefas com base em projetos específicos.

## Funcionalidades

- **Cadastro de Tarefas**: Criar novas tarefas associadas a um projeto.
- **Atualização de Tarefas**: Alterar o status ou as informações de uma tarefa.
- **Gerenciamento de Projetos**: Criar e atualizar projetos.
- **Controle de Prioridade**: As tarefas possuem diferentes níveis de prioridade.

## Tecnologias Utilizadas

- **.NET 8.0**: Framework utilizado para o desenvolvimento da API.
- **Docker**: Para contêinerizar a aplicação e facilitar o deploy.
- **PostgreSQL**: Sistema de gerenciamento de banco de dados utilizado.
- **Swagger**: Ferramenta para testar a API de forma visual e intuitiva.

## Estrutura do Projeto

A estrutura de pastas do projeto é organizada da seguinte forma:

```
TaskManagement/               # Raiz da solução
│
├── Api/                      # Projeto da API
├── Application/              # Lógica de negócio
├── Domain/                   # Entidades e objetos de valor
├── Infrastructure/           # Acesso ao banco de dados e persistência
├── Tests/                    # Testes unitários e de integração
└── docker-compose.yml        # Arquivo para configurar e rodar a aplicação via Docker
```

- **Api**: Contém os controladores da API que gerenciam as requisições HTTP.
- **Application**: Contém a lógica de negócio, serviços e regras de validação.
- **Domain**: Contém as entidades e objetos de valor do domínio do problema.
- **Infrastructure**: Contém a implementação de persistência de dados, incluindo repositórios e conexões com o banco de dados.
- **Tests**: Contém os testes automatizados, tanto unitários quanto de integração.

## Executando o Projeto

Para rodar a aplicação localmente, utilize o Docker com o comando abaixo:

1. **Certifique-se de que o Docker esteja instalado** em seu sistema.
2. **Clone o repositório**:

   ```bash
   git clone https://github.com/joseantonioneves/Teste-EclipseWorks-TaskManager.git
   cd Teste-EclipseWorks-TaskManager
   ```

3. **Construa e execute os containers**:

   ```bash
   docker-compose up --build
   ```

   Isso irá construir as imagens Docker e iniciar os containers necessários.

4. A API estará disponível em `http://localhost:80` e pode ser acessada para testar os endpoints.

## Endpoints da API

A API oferece endpoints para gerenciar projetos e tarefas. Exemplos de endpoints:

- **POST /api/projects**: Cria um novo projeto.
- **POST /api/tasks**: Cria uma nova tarefa.
- **GET /api/projects/{id}**: Recupera detalhes de um projeto específico.
- **GET /api/tasks/{id}**: Recupera detalhes de uma tarefa específica.
- **PUT /api/tasks/{id}**: Atualiza uma tarefa.

A documentação interativa da API pode ser acessada pelo Swagger, em `http://localhost:80/swagger`.

## Testes

Os testes unitários e de integração estão localizados no projeto **Tests**. Para executar os testes, utilize o comando:

```bash
dotnet test
```

Isso irá rodar todos os testes definidos na aplicação e garantir que o sistema esteja funcionando corretamente.


## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

