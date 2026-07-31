# Melhorias de Qualidade — Sistema Gestão Lar

Este documento lista pontos identificados em uma análise geral do projeto que ainda **faltam** para elevar o padrão de qualidade da aplicação (API + Client + Front). Use como checklist/roadmap para evolução do projeto.

## Legenda de prioridade
- 🔴 Alta — impacta segurança, confiabilidade ou manutenção crítica
- 🟡 Média — importante para maturidade do projeto
- 🟢 Baixa — nice to have / polimento

---

## 1. 🔴 Testes automatizados
Não existe nenhum projeto de testes na solução.

- [ ] Criar projeto `SistemaGestaoLar.Api.Tests` (xUnit)
- [ ] Testes unitários para `Services` (MoradorService, AjudanteService, GrupoService, TicketDiarioService, ServicoStatusService)
- [ ] Testes de integração para os `Controllers` usando `WebApplicationFactory` + banco em memória/SQLite in-memory
- [ ] Cobrir cenários de erro (entidade não encontrada, modelo inválido)
- [ ] (Opcional) Testes para componentes Blazor com bUnit

## 2. 🔴 CI/CD
A pasta `.github/workflows` existe mas está vazia.

- [ ] Workflow do GitHub Actions para `dotnet restore/build/test` a cada push/PR
- [ ] Rodar `dotnet format` ou analisadores como parte do pipeline
- [ ] (Opcional) Publicar imagens Docker automaticamente em tags/releases

## 3. 🔴 Autenticação e Autorização
A API atualmente não possui nenhum mecanismo de autenticação — qualquer cliente pode chamar os endpoints.

- [ ] Definir estratégia de auth (JWT, Identity, Entra ID, etc.)
- [ ] Proteger endpoints sensíveis com `[Authorize]`
- [ ] Front-end (Blazor WASM) implementar login/gerenciamento de token

## 4. 🔴 Tratamento global de erros e status HTTP correto
- [ ] Adicionar middleware de exceção global (`UseExceptionHandler` + `ProblemDetails`) para não vazar stack trace em erros 500
- [ ] Corrigir semântica HTTP: `GetById` retorna `BadRequest` (400) quando o recurso não existe — deveria ser `NotFound()` (404)
- [ ] Padronizar respostas de erro em todos os controllers (`AjudantesController`, `GruposController`, `ServicosStatusController`, `TicketDiariosController`)

## 5. 🟡 Validação de modelos
- [ ] Adicionar `DataAnnotations` (`[Required]`, `[MaxLength]`, `[EmailAddress]`, etc.) nos `Models` de entrada
- [ ] Avaliar uso de FluentValidation para regras mais complexas (ex.: datas, disponibilidade de ajudantes)

## 6. 🟡 Logging estruturado
- [ ] Adotar Serilog (ou similar) com sinks configuráveis (console, arquivo)
- [ ] Logar operações de negócio relevantes (criação/edição/exclusão de moradores, tickets, etc.)
- [ ] Adicionar correlação de requisições (`TraceId`) nos logs

## 7. 🟡 Configuração e segredos
- [ ] Remover `database.db` do controle de versão (adicionar ao `.gitignore`) — arquivos de banco não devem ser commitados
- [ ] Externalizar strings de conexão e segredos via variáveis de ambiente / User Secrets / Key Vault em produção
- [ ] Criar `appsettings.Production.json` com configurações apropriadas

## 8. 🟡 Health checks
- [ ] Adicionar `AddHealthChecks()` / endpoint `/health` na API para uso em orquestração (Docker/Kubernetes) e monitoramento

## 9. 🟢 Padronização de código
- [ ] Adicionar `.editorconfig` na raiz da solução para padronizar estilo entre IDEs/desenvolvedores
- [ ] Habilitar analisadores adicionais (`EnableNETAnalyzers`, `TreatWarningsAsErrors` em CI)

## 10. 🟢 Versionamento de API
- [ ] Introduzir versionamento de rotas (`api/v1/...`) para permitir evolução da API sem quebrar o `Api.Client` gerado

---

## Como usar este documento
Trate cada seção como um item de backlog. Recomenda-se atacar primeiro os itens 🔴 (testes, CI/CD, autenticação e tratamento de erros), pois têm maior impacto em confiabilidade e segurança antes de evoluir novas funcionalidades.
