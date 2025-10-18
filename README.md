# 🚀 EasyMoto API — CP5

API de gestão de locações de motos, evoluída a partir do CP4 com **MongoDB**, **Health Checks** e **versionamento via Swagger** (v1/v2).

## 👥 Integrantes
- Valéria Conceição Dos Santos — RM: 557177
- Mirela Pinheiro Silva Rodrigues — RM: 558191
- Guilherme Romanholi Santos — RM: 557462

---

## 🧭 Domínio
Cadastro de **clientes** e **motos**, criação/gestão de **locações** com período e status.

---

## 🆕 O que mudou no CP5
- **MongoDB** integrado (docker/local) e **CRUD completo** para Clientes, Motos e Locações.
- **Health Checks**: `/health`, `/health/live` (liveness) e `/health/ready` (readiness do Mongo).
- **Swagger com versionamento**: v1 e **v2**, com **exemplos de request/response** e **responses por action**.
- **Organização das tags** no Swagger (Health > Clientes > Motos > Locacoes ).
- Manutenção da **Clean Architecture + DDD** do CP4.

---

## ⚙️ Pré‑requisitos
- .NET SDK 8
- MongoDB 

> Adendo: Oracle/EF Core permanecem no projeto para histórico do CP4, mas os **repositórios ativos** utilizam **MongoDB**.

---

## 🔐 Configuração do MongoDB (User Secrets)
Na raiz do **projeto API** (`src/EasyMoto.Api`):

```bash
cd src/EasyMoto.Api
dotnet user-secrets init

# String de conexão (ex.: Atlas)
dotnet user-secrets set "Mongo:ConnectionString" "mongodb+srv://USUARIO:SENHA@SEU-CLUSTER/?retryWrites=true&w=majority"

# Nome do banco
dotnet user-secrets set "Mongo:DatabaseName" "easymoto_cp5"
```

> As chaves são lidas pela API via `MongoSettings`.

---

## ▶️ Como rodar
Na raiz do repositório:

```bash
dotnet restore
dotnet build
dotnet run --project src/EasyMoto.Api
```

Saída esperada (exemplo):
```
Now listening on: http://localhost:5284
```

Acesse o Swagger: **http://localhost:5284/swagger**  
Selecione **EasyMoto API v2** no seletor do topo.

---

## 📚 Swagger
- **v1** e **v2** publicados (versionamento por **segmento de URL**).
- **Exemplos** nos endpoints (via `Swashbuckle.AspNetCore.Filters`).
- **Responses** por action (`[ProducesResponseType]`): 200/201/204/400/404/500.

### Ordem das tags
A UI lista: **Health → Clientes → Motos → Locacoes **.

---

## ❤️ Health Checks
Endpoints (não aparecem no Swagger por padrão, exceto o ping de controller):

- `GET /health` — resumo
- `GET /health/live` — liveness (self)
- `GET /health/ready` — readiness (MongoDB)

Ping documentado (controller):

- `GET /api/v{version}/health/ping` → `"pong"`
---

## 🏗️ Arquitetura (Clean Architecture)
```
src
├─ EasyMoto.Api            # Controllers, DI, Swagger, Health endpoints
├─ EasyMoto.Application    # Casos de uso (Handlers), DTOs (Contracts)
├─ EasyMoto.Domain         # Entidades, Value Objects, interfaces de repositório
└─ EasyMoto.Infrastructure # Mongo (context, documents, repositories) e histórico do EF
```

### DDD
- **Aggregate Root**: `Locacao`
- **Value Object**: `Periodo`
- **Regras principais**
  - `Periodo.Fim > Periodo.Inicio`
  - Moto precisa estar **Disponível** para abrir locação
  - `ClienteId > 0`

---

## 🔗 Endpoints (v2)
Base: `/api/v2`

### Clientes
- `POST /clientes`
- `GET /clientes/{id}`
- `GET /clientes`
- `PUT /clientes/{id}`
- `DELETE /clientes/{id}`

### Motos
- `POST /motos`
- `GET /motos/{id}`
- `GET /motos`
- `PUT /motos/{id}`
- `DELETE /motos/{id}`

### Locações
- `POST /locacoes`
- `GET /locacoes/{id}`
- `GET /locacoes`
- `PUT /locacoes/{id}`
- `DELETE /locacoes/{id}`

---

## 📦 Exemplos de payloads (v2)

### Clientes — POST `/api/v2/clientes`
```json
{
  "nomeCliente": "Marcos Silva",
  "cpfCliente": "04813257860",
  "telefoneCliente": "+55 11 98765-4321",
  "emailCliente": "marcos.silva@example.com"
}
```

### Motos — POST `/api/v2/motos`
```json
{
  "modelo": "Honda CG 160",
  "placa": "ABC1D23",
  "ano": 2023
}
```

### Locações — POST `/api/v2/locacoes`
```json
{
  "clienteId": 1,
  "dataInicio": "2025-09-15T08:00:00-03:00",
  "dataFim": "2025-09-18T18:00:00-03:00",
  "statusLocacao": "Aberta"
}
```

---
