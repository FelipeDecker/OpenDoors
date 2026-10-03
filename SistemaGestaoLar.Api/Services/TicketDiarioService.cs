using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Services
{
    public class TicketDiarioService
    {
        private readonly IGenericRepository<TicketDiario> _repo;

        public TicketDiarioService(IGenericRepository<TicketDiario> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TicketDiario>> GetAllAsync()
        {
            return await _repo.GetQueryableNoTracking()
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoTicket)
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoStatus)
                .ToListAsync();
        }

        public async Task<TicketDiario> GetByIdAsync(int id)
        {
            return await _repo.GetQueryableNoTracking()
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoTicket)
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoStatus)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<TicketDiario> CreateAsync(TicketDiario entity) => _repo.AddAsync(entity);
        public async Task<TicketDiario> UpdateAsync(int id, DateOnly dataServico, IEnumerable<TicketServico> servicos)
        {
            var entidade = await _repo.GetQueryable()
                .Include(t => t.Servicos)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (entidade == null) return null;

            entidade.DataServico = dataServico;

            var novos = servicos.ToList();
            var idsServicoNovos = novos.Select(s => s.ServicoTicketId).ToHashSet();

            // Remove serviços retirados e eventuais duplicados já existentes (mantém apenas um por tipo)
            var paraRemover = entidade.Servicos
                .Where(s => !idsServicoNovos.Contains(s.ServicoTicketId))
                .Concat(entidade.Servicos.GroupBy(s => s.ServicoTicketId).SelectMany(g => g.OrderBy(s => s.Id).Skip(1)))
                .Distinct()
                .ToList();
            foreach (var existente in paraRemover)
            {
                entidade.Servicos.Remove(existente);
            }

            foreach (var novo in novos)
            {
                var existente = entidade.Servicos.FirstOrDefault(s => s.ServicoTicketId == novo.ServicoTicketId);
                if (existente != null)
                {
                    existente.ServicoStatusId = novo.ServicoStatusId;
                }
                else
                {
                    entidade.Servicos.Add(new TicketServico
                    {
                        ServicoTicketId = novo.ServicoTicketId,
                        ServicoStatusId = novo.ServicoStatusId
                    });
                }
            }

            await _repo.SaveChangesAsync();
            return await GetByIdAsync(id);
        }
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
