# Order System

Sistema de gerenciamento de pedidos desenvolvido com .NET 10 utilizando:

- ASP.NET Core Web API
- SQL Server
- MongoDB Cache
- RabbitMQ
- Docker
- xUnit
- Moq
- Observabilidade com Serilog

---

# Arquitetura

O projeto segue princípios de Clean Architecture com separação em camadas:

```text
Api
Application
Domain
Infrastructure
UnitTests
```

---

# Tecnologias Utilizadas

## Backend

- .NET 10
- ASP.NET Core
- Entity Framework Core
- SQL Server
- MongoDB
- RabbitMQ

## Testes

- xUnit
- Moq

## Observabilidade

- Serilog

## Containers

- Docker
- Docker Compose

---

# Funcionalidades

- Criar pedidos
- Buscar pedido por ID
- Listar pedidos
- Cache com MongoDB
- Mensageria com RabbitMQ
- Logs estruturados
- Testes unitários

---

# Como executar o projeto

## Pré-requisitos

### Windows

Instalar:

- Docker Desktop
- .NET 10 SDK

### Linux

Instalar:

- Docker
- Docker Compose
- .NET 10 SDK

---

# Subindo tudo com Docker

Na raiz do projeto:

```bash
docker compose up --build
```

---

# Banco de Dados (Migrations - Entity Framework)

1. Instalar ferramenta do EF (caso não tenha)

```bash
dotnet tool install --global dotnet-ef
```

2. Criar migration inicial
   Execute dentro da pasta do projeto onde está o DbContext (Infrastructure ou Api):

```bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api
```

3. Aplicar migration no banco
   Execute dentro da pasta do projeto onde está o DbContext (Infrastructure ou Api):

```bash
dotnet ef database update --project Infrastructure --startup-project Api
```

---

# Serviços

| Serviço             | Porta |
| ------------------- | ----- |
| API                 | 5278  |
| SQL Server          | 1433  |
| MongoDB             | 27017 |
| RabbitMQ            | 5672  |
| RabbitMQ Management | 15672 |

---

# RabbitMQ Management

Acesse:

```text
http://localhost:15672
```

Usuário:

```text
guest
```

Senha:

```text
guest
```

---

# Swagger

Acesse:

```text
http://localhost:5278/swagger
```

---

# Executando sem Docker

## 1. Subir dependências manualmente

Você precisará:

- SQL Server
- MongoDB
- RabbitMQ

---

## 2. Configurar appsettings.json

```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=localhost,1433;Database=OrdersDb;User Id=sa;Password=senha123;TrustServerCertificate=True"
  },

  "MongoSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "OrdersCacheDb",
    "OrdersCollection": "orders"
  }
}
```

---

## 3. Rodar API

```bash
dotnet run --project Api
```

---

# Executando Testes

```bash
dotnet test
```

---

# Estrutura do Projeto

```text
OrderSystem/
│
├── Api/
├── Application/
├── Domain/
├── Infrastructure/
├── UnitTests/
├── docker-compose.yml
└── README.md
```

---

# Observabilidade

O projeto utiliza Serilog para logging estruturado.

Os logs incluem:

- Requisições HTTP
- Erros
- Fluxo da aplicação

---

# Cache

O sistema utiliza MongoDB como camada de cache para consultas de pedidos.

---

# Mensageria

O sistema utiliza RabbitMQ para publicação de eventos de criação de pedidos.

---

# Melhorias Futuras

- Autenticação JWT
- Retry Policy
- Circuit Breaker
- CI/CD
- Testes de integração
- Health Checks
