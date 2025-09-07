# 🚀 EasyMoto API — CP4

## 👥 Integrantes:
- Valéria Conceição Dos Santos -- RM: 557177
- Mirela Pinheiro Silva Rodrigues -- RM: 558191

## 🧭 Descrição do domínio:
API para gestão de locações de motos: cadastro de clientes e motos, criação de locações com período e status.

## ▶️ Como executar:
**Pré-requisitos**
- .NET SDK 8.0+
- Oracle Database (XE ou equivalente)
- EF Core CLI (`dotnet-ef`)

**Configurar a string de conexão**
- `appsettings.json` (exemplo):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=USUARIO;Password=SENHA;Data Source=localhost:1521/XE"
  }
}
```

**Aplicar migrations**
```bash
dotnet ef database update --project src/EasyMoto.Infrastructure --startup-project src/EasyMoto.Api
```

**Rodar a API**
```bash
dotnet run --project src/EasyMoto.Api
```

## 🏗️ Arquitetura (Clean Architecture)
`Api` | `Application` | `Domain` | `Infrastructure`

- `EasyMoto.Api/` — Controllers, DI
- `EasyMoto.Application/` — Casos de uso (handlers), DTOs (Contracts)  
- `EasyMoto.Domain/` — Entidades, Value Objects, Interfaces de Repositório  
- `EasyMoto.Infrastructure/` — EF Core (DbContext, Configurations, Repositories, Migrations)

## 🧠 DDD (resumo)
- **Aggregate Root**: `Locacao`  
- **Value Objects**: `Periodo`, `Cpf`  
- **Invariantes**:
  - `Periodo.Fim` > `Periodo.Inicio`
  - Moto deve estar **"Disponivel"** ao abrir locação
  - `ClienteId` > 0

## 📜 Exemplos rápidos (POST)

**Clientes** — `POST /api/clientes`
```json
{
  "nomeCliente": "Marcos Silva",
  "cpfCliente": "04813257860",
  "telefoneCliente": "11987654321",
  "emailCliente": "marcos.silva@example.com"
}
```

**Motos** — `POST /api/motos`
```json
{
  "modelo": "YBR 125",
  "placa": "XYZ9A10",
  "ano": 2023,
  "status": "Disponivel"
}
```

**Locações** — `POST /api/locacoes`
```json
{
  "clienteId": 2,
  "dataInicio": "2025-09-15T08:00:00-03:00",
  "dataFim": "2025-09-18T18:00:00-03:00",
  "statusLocacao": "Aberta"
}
```
