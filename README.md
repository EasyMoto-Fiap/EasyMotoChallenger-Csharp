# 🚦 EasyMoto API – FIAP Challenger (Sprint 4 — .NET 8)

**EasyMoto** é uma API RESTful para gestão de **filiais, usuários, motos**, **legendas de status** e **notificações** — voltada ao cenário de operação/locação de frotas.

---

## 🆕 Sprint 4
- 🔐 **Segurança por API Key** (via **.NET User Secrets**; header padrão `X-Api-Key`)
- 🍃 **MongoDB** como banco principal (MongoDB Desktop/Compass ou serviço local)
- 🔢 **Versionamento da API** (header `x-api-version`, padrão **1.0**)
- 🧠 **Endpoint com ML.NET** (ex.: previsão de manutenção)
- ❤️ **Health Checks** (app e dependências, incluindo Mongo)
- 🧪 **Testes**: xUnit (unidade) + **WebApplicationFactory** (integração)
- 📄 **Swagger** com summaries e examples por recurso

---

## 🛠️ Tecnologias
- 🟦 **.NET 8** / ASP.NET Core Web API
- 🍃 **MongoDB** + **MongoDB .NET Driver**
- 🧠 **ML.NET**
- 🧪 **xUnit** + **WebApplicationFactory**
- 🧭 **HATEOAS** (links nos recursos)
- 📄 **Swagger/OpenAPI** (+ `Swashbuckle.AspNetCore.Annotations` e `Filters`)
- 🔐 **API Key** com **.NET User Secrets**

---

## 🧱 Arquitetura (camadas)
- **Domain** – entidades e contratos de repositório
- **Application** – DTOs e *use cases*
- **Infrastructure** – persistência (MongoDB) e repositórios
- **API** – Controllers, Swagger, segurança, versionamento e middlewares

---

## 📦 Exemplos de Payload (POST)

### Filial
```json
{
  "nome": "Filial Centro",
  "cep": "01001-000",
  "cidade": "São Paulo",
  "uf": "SP"
}
```

### Usuário
```json
{
  "nomeCompleto": "Ana Operadora",
  "email": "ana.operadora@example.com",
  "telefone": "11 99999-9999",
  "cpf": "12345678909",
  "cepFilial": "01001-000",
  "senha": "SenhaForte@123",
  "confirmarSenha": "SenhaForte@123",
  "perfil": 0,
  "ativo": true,
  "filialId": 1
}
```
> `perfil`: `0=OPERADOR`, `1=ADMIN`

### Moto
```json
{
  "placa": "ABC1D23",
  "modelo": "Honda CG 160 Fan",
  "ano": 2022,
  "cor": "Preta",
  "ativo": true,
  "filialId": 1,
  "categoria": 0,
  "statusOperacional": 0,
  "legendaStatusId": 2,
  "qrCode": "MOTO-ABC1D23"
}
```
> `categoria`: `0=POP`, `1=SPORT`, `2=E`  
> `statusOperacional`: `0=DISPONIVEL`, `1=ALUGADA`, `2=MANUTENCAO`

### LegendaStatus
```json
{
  "titulo": "Disponível",
  "descricao": "Moto pronta para uso",
  "corHex": "#28A745",
  "ativo": true
}
```

### Notificação
```json
{
  "tipo": 0,
  "mensagem": "Moto cadastrada",
  "motoId": 1,
  "usuarioOrigemId": 3,
  "escopo": 0
}
```

### Marcar Notificação como Lida
`POST /api/notificacoes/{id}/marcar-lida`
```json
{
  "usuarioId": 3
}
```

---

## 🔐 Autenticação (API Key) e Versionamento

- **API Key** no header:
  ```
  X-Api-Key: SUA_CHAVE
  ```

- **Versão da API** (opcional; padrão `1.0`):
  ```
  x-api-version: 1.0
  ```

Exemplo `curl`:
```bash
curl -X GET http://localhost:5230/api/motos   -H "X-Api-Key: SUA_CHAVE"   -H "x-api-version: 1.0"
```

---

## ⚙️ Como rodar (MongoDB + User Secrets)

### 1) Pré-requisitos
- .NET 8 SDK
- MongoDB rodando em `mongodb://localhost:27017` (MongoDB Desktop)

### 2) Clonar
```bash
git clone https://github.com/valor-null/EasyMotoChallenger-Csharp.git
cd EasyMotoChallenger-Csharp
```

### 3) Configurar **.NET User Secrets** (no projeto da API)
```bash
cd src/EasyMoto.Api
dotnet user-secrets init
dotnet user-secrets set "Auth:ApiKey" "SUA_CHAVE_AQUI"
dotnet user-secrets set "Auth:HeaderName" "X-Api-Key"
dotnet user-secrets set "Mongo:ConnectionString" "mongodb://localhost:27017"
dotnet user-secrets set "Mongo:Database" "easymoto_dev"
```

### 4) Executar
```bash
dotnet restore
dotnet run --project src/EasyMoto.Api
```
- API: `http://localhost:5230`
- Swagger: `http://localhost:5230/swagger`
- Health: `http://localhost:5230/health`

---

## 🪷 Executar em Development (Windows/PowerShell)

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run --project src/EasyMoto.Api/EasyMoto.Api.csproj
```

### Criar/definir a API Key (User Secrets do projeto da API)
```
dotnet user-secrets init --project src/EasyMoto.Api/EasyMoto.Api.csproj
dotnet user-secrets set  --project src/EasyMoto.Api/EasyMoto.Api.csproj "Auth:ApiKey" "EM_3f8b2a4c9b7e4e6e9a7c1f2d3a5b8c"
dotnet user-secrets set  --project src/EasyMoto.Api/EasyMoto.Api.csproj "Auth:HeaderName" "X-Api-Key"
dotnet user-secrets set  --project src/EasyMoto.Api/EasyMoto.Api.csproj "Mongo:ConnectionString" "mongodb://localhost:27017"
dotnet user-secrets set  --project src/EasyMoto.Api/EasyMoto.Api.csproj "Mongo:Database" "easymoto_dev"
dotnet user-secrets list --project src/EasyMoto.Api/EasyMoto.Api.csproj
```
---

## 🧪 Testes
```bash
dotnet test -v minimal
```

---

## 👩‍💻 Integrantes
- **Valéria Conceição Dos Santos** — RM: **557177**
- **Mirela Pinheiro Silva Rodrigues** — RM: **558191**
