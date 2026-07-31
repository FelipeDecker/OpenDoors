# Sistema Gestão Lar

Sistema web para **gestão de um lar/casa de acolhimento**, permitindo o controle de moradores, ajudantes (voluntários/colaboradores), grupos de trabalho e os serviços diários prestados a cada morador.

## Finalidade do projeto

O objetivo do sistema é centralizar e organizar as informações operacionais de um lar de acolhimento, facilitando o acompanhamento diário dos cuidados prestados aos moradores e a organização das equipes de ajudantes responsáveis por esses cuidados.

## O que o sistema faz

- **Gestão de Moradores**: cadastro de moradores com nome completo, data de nascimento, contato de emergência, observações e histórico de acolhimento.
- **Gestão de Ajudantes**: cadastro de ajudantes/voluntários com nome, e-mail, telefone e disponibilidade, podendo ser organizados em grupos.
- **Gestão de Grupos**: criação de grupos de ajudantes, com nome e descrição, para organizar equipes de trabalho.
- **Tickets Diários de Serviço**: para cada morador, é possível registrar um ticket diário contendo os serviços realizados (ex.: Jantar, Banho, Troca de Roupas), cada um com um status de acompanhamento (Pendente, Realizado, Não Realizado).
- **API documentada**: exposição de um cliente HTTP tipado (gerado via NSwag) consumido pela aplicação web, além de documentação OpenAPI/Swagger em ambiente de desenvolvimento.

## Arquitetura e tecnologias

O projeto é organizado em três aplicações .NET 10:

| Projeto | Descrição |
|---|---|
| `SistemaGestaoLar.Api` | API REST (ASP.NET Core) responsável pelas regras de negócio e persistência dos dados. Utiliza Entity Framework Core com SQLite e aplica as migrations automaticamente na inicialização. Expõe documentação OpenAPI/Swagger. |
| `SistemaGestaoLar.Api.Client` | Biblioteca de cliente HTTP tipado (gerado com NSwag) para consumo da API pela aplicação front-end. |
| `SistemaGestaoLar.Front` | Aplicação Blazor WebAssembly que fornece a interface web para gerenciamento de moradores, ajudantes, grupos e tickets diários. |

### Principais entidades

- **Morador**: pessoa acolhida no lar.
- **Ajudante**: voluntário/colaborador responsável pelos cuidados.
- **Grupo**: agrupamento de ajudantes.
- **TicketDiario**: registro diário de serviços prestados a um morador.
- **TicketServico**: serviço específico (Jantar, Banho, Troca de Roupas) vinculado a um ticket diário, com seu respectivo status (Pendente, Realizado, Não Realizado).

## Como executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Executando a API

```bash
cd SistemaGestaoLar.Api
dotnet run
```

A API aplica automaticamente as migrations do banco de dados SQLite (`database.db`) na inicialização e disponibiliza a documentação Swagger em ambiente de desenvolvimento.

### Executando o Front-end

```bash
cd SistemaGestaoLar.Front
dotnet run
```

A aplicação Blazor WebAssembly consome a API através da configuração `GestaoLarApi` (URL base da API), definida em seus arquivos de configuração.

### Docker

Ambos os projetos (`SistemaGestaoLar.Api` e `SistemaGestaoLar.Front`) possuem `Dockerfile` próprio para build e execução em contêiner.
