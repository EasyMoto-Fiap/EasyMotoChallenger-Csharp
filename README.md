# 🚦 EasyMoto API – FIAP Challenger (Sprint 3 — Advanced .NET 8)

**EasyMoto** é uma API RESTful para gerenciamento de **filiais, usuários, motos** e **notificações**, pensada para o contexto de locação e operação de frotas.  
Foco em **boas práticas REST**, **paginações**, **HATEOAS**, **status codes adequados** e **Swagger/OpenAPI com exemplos**.

## 🛠️ Tecnologias

- 🟦 **.NET 8** / ASP.NET Core Web API  
- 🗃️ **Entity Framework Core** (EF Core)  
- 🐘 **PostgreSQL** (provider **Npgsql**)  
- 📦 **EF Core Migrations**  
- 🧭 **HATEOAS** (links nos responses)  
- 📄 **Swagger/OpenAPI** (+ exemplos com *Swashbuckle.AspNetCore.Filters*)  

---

## 🧩 Domínio e Entidades

> Justificativa do domínio: o gerenciamento de motos em múltiplas filiais exige cadastro padronizado, status operacionais claros e comunicação de eventos (notificações) para operadores, garantindo rastreabilidade e escala.

### Entidades principais:

- **Filial**: `Id, Nome, Cep, Cidade, UF`  
- **Usuario** (Operador/Admin): `Id, Nome, Email, SenhaHash, Cpf, Telefone, Role, FilialId, Ativo`  
- **Moto**: `Id, Placa, Modelo, Ano, Cor, Ativo, FilialId, Categoria, StatusOperacional, LegendaStatusId?, QrCode, CreatedAt, UpdatedAt`  

### Entidades de apoio:
- **LegendaStatus** (catálogo de status com legenda): `Id, Nome, Descricao`  
- **Notificacao** (eventos como “moto cadastrada”): `Id, Tipo, Mensagem, Escopo, MotoId?, UsuarioOrigemId, CriadaEm`  
- **Leitura de Notificação** (marcação de lido por usuário): `NotificacaoId, UsuarioId, LidoEm`

### Relacionamentos:

- `Usuario (N)–(1) Filial`  
- `Moto (N)–(1) LegendaStatus`  
- `Notificacao (N)–(0..1) Moto`  
- `Notificacao (N)–(1) UsuarioOrigem`  
- `NotificacaoLeitura (N)–(1) Notificacao` e `(N)–(1) Usuario`

---

## 🔗 Endpoints (principais)

Rotas `api/[controller]`.:

### Filiais
```
GET    /api/filiais?page=1&pageSize=10
GET    /api/filiais/{id}
POST   /api/filiais
PUT    /api/filiais/{id}
DELETE /api/filiais/{id}
```

### Usuários
```
GET    /api/usuarios?page=1&pageSize=10
GET    /api/usuarios/{id}
POST   /api/usuarios
PUT    /api/usuarios/{id}
DELETE /api/usuarios/{id}
```

### Motos
```
GET    /api/motos?page=1&pageSize=10
GET    /api/motos/{id}
POST   /api/motos
PUT    /api/motos/{id}
DELETE /api/motos/{id}
```

### Legendas de Status
```
GET    /api/legendasstatus?page=1&pageSize=10
GET    /api/legendasstatus/{id}
POST   /api/legendasstatus
PUT    /api/legendasstatus/{id}
DELETE /api/legendasstatus/{id}
```

### Notificações
```
GET    /api/notificacoes?page=1&pageSize=10
GET    /api/notificacoes/{id}
POST   /api/notificacoes
DELETE /api/notificacoes/{id}
POST   /api/notificacoes/{id}/marcar-lida
```
---

## 📦 Exemplos de Payload

### Filial (POST)
```json
{
  "nome": "Filial Centro",
  "cep": "01001-000",
  "cidade": "São Paulo",
  "uf": "SP"
}
```

### Usuário (POST)
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
> `perfil`: `0=OPERADOR`, `1=ADMIN`.

### Moto (POST)
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

### LegendaStatus (POST)
```json
{
  "nome": "Disponível",
  "descricao": "Moto pronta para uso"
}
```

### Notificacao (POST)
```json
{
  "tipo": 0,
  "mensagem": "Moto cadastrada",
  "escopo": 0,
  "motoId": 1,
  "usuarioOrigemId": 3
}
```
> `tipo`: por exemplo `0=MOTO_CADASTRADA`, `1=MOTO_ATUALIZADA`  
> `escopo`: `0=GLOBAL`, `1=USUARIO`, `2=FILIAL`

### Marcar Notificação como Lida
`POST /api/notificacoes/{id}/marcar-lida`
```json
{
  "usuarioId": 3
}
```

---

## 🚀 Como rodar o projeto

### 1) Clonar
```bash
git clone https://github.com/valor-null/EasyMotoChallenger-Csharp.git
cd EasyMotoChallenger-Csharp
```

### 2) Configurar conexão com o PostgreSQL
Edite `src/EasyMoto.Api/appsettings.Development.json` (ou variável de ambiente) e ajuste a sua connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```

### 3) Restaurar, migrar e executar
```bash
dotnet restore
dotnet tool install --global dotnet-ef # se ainda não tiver
dotnet ef database update -p src/EasyMoto.Infrastructure -s src/EasyMoto.Api
dotnet run --project src/EasyMoto.Api
```

- A API sobe em `http://localhost:5230`  
- Swagger: `http://localhost:5230/swagger`


## 🧱 Arquitetura (camadas)

- **Domain**: entidades e contratos de repositório  
- **Application**: DTOs, *use cases* (casos de uso), *mappers* e validações de orquestração  
- **Infrastructure**: EF Core (`DbContext`), mapeamentos (`Configurations`), repositórios  
- **API**: Controllers, configuração do Swagger e exposição HTTP

---

## 👩‍💻 Integrantes

- 💁‍♀️ **Valéria Conceição Dos Santos** — RM: **557177**  
- 💁‍♀️ **Mirela Pinheiro Silva Rodrigues** — RM: **558191**
- 💁‍♀️ **Luiz Eduardo Da Silva Pinto** — RM: **555213**
---
