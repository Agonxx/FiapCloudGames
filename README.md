# FIAP Cloud Games - Tech Challenge

API REST para gerenciamento de uma plataforma de jogos digitais, desenvolvida como Tech Challenge da FIAP.

## Tecnologias

- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- xUnit + Moq (Testes)
- Swagger/OpenAPI

## Arquitetura

O projeto segue uma arquitetura em camadas:

```
FiapCloudGames/
├── FiapCloudGames.API            # Controllers, Middlewares, Configurações
├── FiapCloudGames.Application    # Services (regras de negócio)
├── FiapCloudGames.Domain         # Entidades, Interfaces, DTOs, Enums
├── FiapCloudGames.Infrastructure # Repositories, DbContext, Configurações EF
└── FiapCloudGames.Tests          # Testes unitários
```

## Configuração

### 1. Connection String

Configure a connection string no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=FiapCloudGames;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 2. JWT Settings

Configure as opções de JWT no `appsettings.json`:

```json
{
  "JwtSettings": {
    "Secret": "SUA_CHAVE_SECRETA_COM_NO_MINIMO_32_CARACTERES",
    "ExpiracaoHoras": 8
  }
}
```

## Migrations

### Pré-requisitos

Instale a ferramenta do Entity Framework Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

### Criar uma nova Migration

```bash
dotnet ef migrations add NomeDaMigration --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API
```

### Aplicar Migrations no Banco

```bash
dotnet ef database update --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API
```

### Remover última Migration (se ainda não aplicada)

```bash
dotnet ef migrations remove --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API
```

### Gerar Script SQL

```bash
dotnet ef migrations script --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API -o script.sql
```

## Executando o Projeto

### Build

```bash
dotnet build
```

### Executar API

```bash
dotnet run --project FiapCloudGames.API
```

### Executar Testes

```bash
dotnet test
```

## Recursos da API

### Autenticação

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/usuario/login` | Autenticação (retorna JWT) |

### Usuários

| Método | Endpoint | Acesso | Descrição |
|--------|----------|--------|-----------|
| GET | `/api/usuario` | Admin | Lista todos os usuários |
| GET | `/api/usuario/{id}` | Admin | Busca usuário por ID |
| GET | `/api/usuario/me` | Autenticado | Dados do usuário logado |
| POST | `/api/usuario` | Público | Cadastro de usuário |
| PUT | `/api/usuario` | Admin | Atualiza usuário |
| DELETE | `/api/usuario/{id}` | Admin | Remove usuário |

### Jogos

| Método | Endpoint | Acesso | Descrição |
|--------|----------|--------|-----------|
| GET | `/api/jogo` | Autenticado | Lista todos os jogos |
| GET | `/api/jogo/{id}` | Autenticado | Busca jogo por ID |
| POST | `/api/jogo` | Admin | Cadastra novo jogo |
| PUT | `/api/jogo` | Admin | Atualiza jogo |
| DELETE | `/api/jogo/{id}` | Admin | Remove jogo |

### Biblioteca

| Método | Endpoint | Acesso | Descrição |
|--------|----------|--------|-----------|
| GET | `/api/biblioteca` | Autenticado | Jogos do usuário logado |
| GET | `/api/biblioteca/{id}` | Admin | Jogos de um usuário específico |
| POST | `/api/biblioteca/{jogoId}` | Autenticado | Comprar jogo |

## Usuários Padrão (Seed)

O banco é inicializado com dois usuários:

| Email | Senha | Nível |
|-------|-------|-------|
| admin@fcg.com | A123 | Administrador |
| user@fcg.com | U123 | Usuário |

## Swagger

A documentação interativa está disponível em `/swagger` quando executado em ambiente de desenvolvimento.

## Estrutura do Banco de Dados

```
Usuarios (1) ─────┐
                  │
                  ▼
            ItensBiblioteca (N)
                  │
                  ▲
Jogos (N) ────────┘
```

- **Usuarios**: Dados dos usuários com autenticação
- **Jogos**: Catálogo de jogos disponíveis
- **ItensBiblioteca**: Relacionamento N:N entre usuários e jogos adquiridos

## Middlewares

| Middleware | Descrição |
|------------|-----------|
| CorrelationIdMiddleware | Adiciona ID único para rastreamento de requisições |
| ExceptionMiddleware | Tratamento centralizado de exceções |
| RequestLoggingMiddleware | Log de requisições com tempo de execução |
| JwtMiddleware | Extração de claims do token JWT |

## Testes

O projeto inclui testes unitários para os Services:

- `JogoServiceTests` - Testes do CRUD de jogos
- `UsuarioServiceTests` - Testes de autenticação e CRUD de usuários
- `BibliotecaServiceTests` - Testes de compra e listagem de jogos

## Licença

Projeto desenvolvido para fins acadêmicos - Tech Challenge FIAP.
