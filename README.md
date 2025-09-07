# 🚀 EasyMoto API — CP4

## 👥 Integrantes:
- Valéria Conceição Dos Santos -- RM: 557177
- Mirela Pinheiro Silva Rodrigues -- RM: 558191

## 🧭 Descrição do domínio:
API para gestão de locações de motos: cadastro de clientes e motos, criação de locações com período e status.

## ▶️ Como rodar o projeto:

### 0) Pré‑requisitos
- .NET SDK 8 instalado
- Acesso ao Oracle Sql Developer

### 1) Ir para a raiz do repositório

```bash
dotnet tool update --global dotnet-ef
```

### 2) Configurar a connection string
**appsettings.json**  
Edite `src/EasyMoto.Api/appsettings.json` e adicione:
```json
{
  "Logging": { "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" } },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl; User Id=SEU_USUARIO; Password=SUA_SENHA;"
  }
}
```

### 3) Restore e build
```powershell
dotnet restore
dotnet build
```

### 4) Aplicar migrations
> Se as tabelas já existirem, o comando apenas sincroniza o histórico.
```powershell
dotnet ef database update `
  --project src/EasyMoto.Infrastructure/EasyMoto.Infrastructure.csproj `
  --startup-project src/EasyMoto.Api/EasyMoto.Api.csproj
```

### 5) Rodar a API
```powershell
dotnet run --project src/EasyMoto.Api/EasyMoto.Api.csproj
```

### Resultado esperado
No console:
```
Now listening on: https://localhost:7230
Now listening on: http://localhost:5284
```
Acesse: `http://localhost:5284/swagger`

## 🏗️ Arquitetura (Clean Architecture)
`Api` | `Application` | `Domain` | `Infrastructure`

- `EasyMoto.Api/` — Controllers, DI
- `EasyMoto.Application/` — Casos de uso (handlers), DTOs (Contracts)  
- `EasyMoto.Domain/` — Entidades, Value Objects, Interfaces de Repositório  
- `EasyMoto.Infrastructure/` — EF Core (DbContext, Configurations, Repositories, Migrations)

## 🧠 DDD 
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

