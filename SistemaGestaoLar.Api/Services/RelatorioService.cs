using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Enums;
using SistemaGestaoLar.Api.Helpers;

namespace SistemaGestaoLar.Api.Services
{
    public class RelatorioService
    {
        private static readonly string[] NomesMeses =
        [
            "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
            "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
        ];

        private readonly IGenericRepository<Morador> _moradorRepo;
        private readonly IGenericRepository<TicketDiario> _ticketRepo;

        public RelatorioService(IGenericRepository<Morador> moradorRepo, IGenericRepository<TicketDiario> ticketRepo)
        {
            _moradorRepo = moradorRepo;
            _ticketRepo = ticketRepo;
        }

        public static string NomeMes(int mes) => NomesMeses[mes - 1];

        public async Task<(byte[] Conteudo, string NomeArquivo)> GerarRelatorioMesAnteriorAsync()
        {
            var inicioMesAtual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var inicioMesAnterior = inicioMesAtual.AddMonths(-1);
            var dataInicio = DateOnly.FromDateTime(inicioMesAnterior);
            var dataFim = DateOnly.FromDateTime(inicioMesAtual);

            var moradores = await _moradorRepo.GetQueryableNoTracking()
                .Where(m => m.DataCadastro < inicioMesAtual)
                .OrderBy(m => m.Id)
                .ToListAsync();

            var realizado = ServicoStatusEnum.Realizado.ToString();
            var tickets = await _ticketRepo.GetQueryableNoTracking()
                .Where(t => t.DataServico >= dataInicio && t.DataServico < dataFim)
                .Select(t => new
                {
                    t.MoradorId,
                    t.DataServico,
                    Servicos = t.Servicos
                        .Where(s => s.ServicoStatus.Name == realizado)
                        .Select(s => s.ServicoTicket.Name)
                        .ToList()
                })
                .ToListAsync();

            var nomesMoradores = await _moradorRepo.GetQueryableNoTracking()
                .ToDictionaryAsync(m => m.Id, m => m.NomeCompleto);

            var linhasTickets = tickets
                .Where(t => t.Servicos.Count > 0)
                .GroupBy(t => new { t.MoradorId, t.DataServico })
                .Select(g => new
                {
                    g.Key.MoradorId,
                    Morador = nomesMoradores.GetValueOrDefault(g.Key.MoradorId) ?? string.Empty,
                    Dia = g.Key.DataServico,
                    Servicos = string.Join(";", g
                        .SelectMany(t => t.Servicos)
                        .Distinct()
                        .Select(NomeServico))
                })
                .OrderBy(l => l.Morador)
                .ThenBy(l => l.MoradorId)
                .ThenBy(l => l.Dia)
                .ToList();

            using var workbook = new XLWorkbook();

            var sheetMoradores = workbook.Worksheets.Add("Moradores");
            string[] cabecalhoMoradores = ["Id", "Nome completo", "Data de nascimento", "Contato de emergência", "Observações"];
            EscreverCabecalho(sheetMoradores, cabecalhoMoradores);
            var linha = 2;
            foreach (var m in moradores)
            {
                sheetMoradores.Cell(linha, 1).Value = m.Id;
                sheetMoradores.Cell(linha, 2).Value = m.NomeCompleto;
                if (m.DataNascimento.HasValue)
                {
                    sheetMoradores.Cell(linha, 3).Value = m.DataNascimento.Value.Date;
                    sheetMoradores.Cell(linha, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                }
                sheetMoradores.Cell(linha, 4).Value = m.ContatoEmergencia;
                sheetMoradores.Cell(linha, 5).Value = m.Observacoes;
                linha++;
            }
            sheetMoradores.Columns().AdjustToContents();

            var sheetTickets = workbook.Worksheets.Add("Tickets");
            string[] cabecalhoTickets = ["Id", "Morador", "Dia", "Serviços"];
            EscreverCabecalho(sheetTickets, cabecalhoTickets);
            linha = 2;
            foreach (var l in linhasTickets)
            {
                sheetTickets.Cell(linha, 1).Value = l.MoradorId;
                sheetTickets.Cell(linha, 2).Value = l.Morador;
                sheetTickets.Cell(linha, 3).Value = l.Dia.ToDateTime(TimeOnly.MinValue);
                sheetTickets.Cell(linha, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                sheetTickets.Cell(linha, 4).Value = l.Servicos;
                linha++;
            }
            sheetTickets.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            var nomeArquivo = $"Relatorio_{NomeMes(inicioMesAnterior.Month)}_{inicioMesAnterior.Year}.xlsx";
            return (stream.ToArray(), nomeArquivo);
        }

        private static void EscreverCabecalho(IXLWorksheet sheet, string[] colunas)
        {
            for (var i = 0; i < colunas.Length; i++)
            {
                sheet.Cell(1, i + 1).Value = colunas[i];
            }
            sheet.Row(1).Style.Font.Bold = true;
        }

        private static string NomeServico(string nome) =>
            Enum.TryParse<ServicoTicketEnum>(nome, out var servico)
                ? EnumDisplayHelper.GetDisplayName(servico)
                : nome;
    }
}
