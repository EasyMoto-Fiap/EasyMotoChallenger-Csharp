# 🚦 EasyMoto API – FIAP Challenger

EasyMoto é uma API RESTful desenvolvida como solução para um desafio real da Mottu.  
O objetivo é facilitar o mapeamento inteligente de pátios, o gerenciamento de motos e toda a jornada de aluguel e localização em múltiplas filiais, trazendo escalabilidade, performance e integração real.

---

## 🛠️ Tecnologias Utilizadas

- 🟦 .NET 8 / ASP.NET Core
- 🗃️ Entity Framework Core
- 📦 EF Core Migrations
- 🔄 AutoMapper
- 📄 Swagger/OpenAPI
- 🛢️ Oracle SQL Developer

---

## 🗂️ Entidades e Relacionamentos

O projeto implementa as seguintes entidades, com relacionamentos via chave estrangeira conforme a modelagem Oracle:

- Cliente
- Empresa
- Filial (relacionada a Empresa)
- Funcionario (relacionado a Filial)
- Operador (relacionado a Filial)
- Patio (relacionado a Filial)
- ClienteLocacao (relacionada a Cliente)
- Localizacao
- Moto (relacionada a ClienteLocacao e Localizacao)
- Vaga (relacionada a Moto e Patio)

Cada entidade possui CRUD completo (GET, GET/{id}, POST, PUT, DELETE).

---

## 🔗 Rotas Principais (Exemplo Cliente)

- `GET    /api/Cliente`
- `GET    /api/Cliente/{id}`
- `POST   /api/Cliente`
- `PUT    /api/Cliente/{id}`
- `DELETE /api/Cliente/{id}`

Demais entidades possuem as mesmas rotas padrão REST, trocando "Cliente" por Empresa, Filial, Funcionario, Operador, Patio, ClienteLocacao, Localizacao, Moto e Vaga.

---

## 🚀 Como rodar o projeto

1. **Clone o repositório:**
   ```bash
   https://github.com/akemilol/EasyMotoChallenger-Csharp.git
   
2. **Configure a string de conexão Oracle:**
 No arquivo `appsettings.json`, insira sua connection string.

3. **Restaure e execute:**
   ```bash
    dotnet restore
    dotnet run
4. **Acesse o Swagger**

## 👩‍💻 Integrantes: 
- 💁‍♀️Valéria Conceição Dos Santos - RM: 557177
- 💁‍♀️Mirela Pinheiro Silva Rodrigues - RM: 558191
